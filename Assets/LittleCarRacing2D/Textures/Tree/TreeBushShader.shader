// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "g51/treeBushShader"
{
	Properties
	{
		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
		_Color ("Tint", Color) = (1,1,1,1)
		[MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
		[PerRendererData] _AlphaTex ("External Alpha", 2D) = "white" {}
		_gradientRadius("gradientRadius", Float) = 0
		_sizeOffset("sizeOffset", Range( 0 , 1)) = -2
		_minimumlAlpha("minimumlAlpha", Range( 0 , 1)) = 0
		_PlayerPosition("PlayerPosition", Vector) = (0,0,0,0)
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
	}

	SubShader
	{
		Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" "PreviewType"="Plane" "CanUseSpriteAtlas"="True" }

		Cull Off
		Lighting Off
		ZWrite Off
		Blend One OneMinusSrcAlpha
		
		
		Pass
		{
		CGPROGRAM
			
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0
			#pragma multi_compile _ PIXELSNAP_ON
			#pragma multi_compile _ ETC1_EXTERNAL_ALPHA
			#include "UnityCG.cginc"
			

			struct appdata_t
			{
				float4 vertex   : POSITION;
				float4 color    : COLOR;
				float2 texcoord : TEXCOORD0;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				
			};

			struct v2f
			{
				float4 vertex   : SV_POSITION;
				fixed4 color    : COLOR;
				float2 texcoord  : TEXCOORD0;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
				float4 ase_texcoord1 : TEXCOORD1;
			};
			
			uniform fixed4 _Color;
			uniform float _EnableExternalAlpha;
			uniform sampler2D _MainTex;
			uniform sampler2D _AlphaTex;
			uniform float4 _MainTex_ST;
			uniform float _minimumlAlpha;
			uniform float2 _PlayerPosition;
			uniform float _gradientRadius;
			uniform float _sizeOffset;
			
			v2f vert( appdata_t IN  )
			{
				v2f OUT;
				UNITY_SETUP_INSTANCE_ID(IN);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);
				UNITY_TRANSFER_INSTANCE_ID(IN, OUT);
				float3 ase_worldPos = mul(unity_ObjectToWorld, IN.vertex).xyz;
				OUT.ase_texcoord1.xyz = ase_worldPos;
				
				
				//setting value to unused interpolator channels and avoid initialization warnings
				OUT.ase_texcoord1.w = 0;
				
				IN.vertex.xyz +=  float3(0,0,0) ; 
				OUT.vertex = UnityObjectToClipPos(IN.vertex);
				OUT.texcoord = IN.texcoord;
				OUT.color = IN.color * _Color;
				#ifdef PIXELSNAP_ON
				OUT.vertex = UnityPixelSnap (OUT.vertex);
				#endif

				return OUT;
			}

			fixed4 SampleSpriteTexture (float2 uv)
			{
				fixed4 color = tex2D (_MainTex, uv);

#if ETC1_EXTERNAL_ALPHA
				// get the color from an external texture (usecase: Alpha support for ETC1 on android)
				fixed4 alpha = tex2D (_AlphaTex, uv);
				color.a = lerp (color.a, alpha.r, _EnableExternalAlpha);
#endif //ETC1_EXTERNAL_ALPHA

				return color;
			}
			
			fixed4 frag(v2f IN  ) : SV_Target
			{
				float2 uv_MainTex = IN.texcoord.xy * _MainTex_ST.xy + _MainTex_ST.zw;
				float4 appendResult23 = (float4(0.0 , 0.0 , 0.0 , _minimumlAlpha));
				float3 ase_worldPos = IN.ase_texcoord1.xyz;
				float2 appendResult9 = (float2(ase_worldPos.x , ase_worldPos.y));
				float temp_output_30_0 = ( distance( appendResult9 , _PlayerPosition ) / _gradientRadius );
				float clampResult50 = clamp( ( temp_output_30_0 - ( (1.0 + (temp_output_30_0 - 0.0) * (0.0 - 1.0) / (1.0 - 0.0)) + (-1.0 + (_sizeOffset - 0.0) * (1.0 - -1.0) / (1.0 - 0.0)) ) ) , 0.0 , 1.0 );
				float4 appendResult16 = (float4(1.0 , 1.0 , 1.0 , clampResult50));
				float4 AlphaMask120 = appendResult16;
				float4 clampResult24 = clamp( ( appendResult23 + AlphaMask120 ) , float4( 1,1,1,0 ) , float4( 1,1,1,1 ) );
				
				fixed4 c = ( IN.color * ( tex2D( _MainTex, uv_MainTex ) * clampResult24 ) );
				c.rgb *= c.a;
				return c;
			}
		ENDCG
		}
	}
	CustomEditor "ASEMaterialInspector"
	
	
}
/*ASEBEGIN
Version=17000
0;0;1920;1019;3062.954;-488.1921;1;True;False
Node;AmplifyShaderEditor.CommentaryNode;26;-2477.674,768.3262;Float;False;2382.006;692.1158;Comment;13;50;46;47;48;49;14;30;18;8;4;9;5;20;;1,1,1,1;0;0
Node;AmplifyShaderEditor.WorldPosInputsNode;5;-2438.843,860.8352;Float;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.DynamicAppendNode;9;-2258.843,884.8352;Float;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.Vector2Node;4;-2308.843,997.8351;Float;False;Property;_PlayerPosition;PlayerPosition;3;0;Create;True;0;0;False;0;0,0;23.06,22.47;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.DistanceOpNode;8;-2086.843,918.8351;Float;True;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;18;-1887.244,985.975;Float;False;Property;_gradientRadius;gradientRadius;0;0;Create;True;0;0;False;0;0;35;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;30;-1678.286,916.3394;Float;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;14;-1740.761,1197.485;Float;False;Property;_sizeOffset;sizeOffset;1;0;Create;True;0;0;False;0;-2;0.2;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;48;-1367.34,1198.325;Float;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;-1;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;49;-1443.178,976.7562;Float;True;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;1;False;4;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;47;-1161.663,1129.97;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;46;-1011.286,921.3394;Float;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;50;-786.2861,920.3394;Float;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;16;-623.2847,851.8525;Float;False;COLOR;4;0;FLOAT;1;False;1;FLOAT;1;False;2;FLOAT;1;False;3;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;20;-485.4856,844.1577;Float;False;AlphaMask1;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;19;-1458.802,105.8124;Float;False;Property;_minimumlAlpha;minimumlAlpha;2;0;Create;True;0;0;False;0;0;0.2;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;21;-1225.936,191.5562;Float;False;20;AlphaMask1;1;0;OBJECT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.DynamicAppendNode;23;-1169.895,42.32953;Float;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SimpleAddOpNode;22;-966.0385,110.0432;Float;False;2;2;0;FLOAT4;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.TemplateShaderPropertyNode;1;-1497.44,-281.9011;Float;False;0;0;_MainTex;Shader;0;5;SAMPLER2D;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;2;-1300.44,-284.9011;Float;True;Property;_TextureSample0;Texture Sample 0;0;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ClampOpNode;24;-801.2426,112.0023;Float;False;3;0;FLOAT4;0,0,0,0;False;1;FLOAT4;1,1,1,0;False;2;FLOAT4;1,1,1,1;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;15;-372.0115,12.60516;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT4;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.VertexColorNode;52;-449.8835,-167.1317;Float;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;51;-250.8835,-137.1317;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;0;0,0;Float;False;True;2;Float;ASEMaterialInspector;0;6;g51/treeBushShader;0f8ba0101102bb14ebf021ddadce9b49;True;SubShader 0 Pass 0;0;0;SubShader 0 Pass 0;2;True;3;1;False;-1;10;False;-1;0;1;False;-1;0;False;-1;False;False;True;2;False;-1;False;False;True;2;False;-1;False;False;True;5;Queue=Transparent=Queue=0;IgnoreProjector=True;RenderType=Transparent=RenderType;PreviewType=Plane;CanUseSpriteAtlas=True;False;0;False;False;False;False;False;False;False;False;False;False;True;2;0;;0;0;Standard;0;0;1;True;False;2;0;FLOAT4;0,0,0,0;False;1;FLOAT3;0,0,0;False;0
WireConnection;9;0;5;1
WireConnection;9;1;5;2
WireConnection;8;0;9;0
WireConnection;8;1;4;0
WireConnection;30;0;8;0
WireConnection;30;1;18;0
WireConnection;48;0;14;0
WireConnection;49;0;30;0
WireConnection;47;0;49;0
WireConnection;47;1;48;0
WireConnection;46;0;30;0
WireConnection;46;1;47;0
WireConnection;50;0;46;0
WireConnection;16;3;50;0
WireConnection;20;0;16;0
WireConnection;23;3;19;0
WireConnection;22;0;23;0
WireConnection;22;1;21;0
WireConnection;2;0;1;0
WireConnection;24;0;22;0
WireConnection;15;0;2;0
WireConnection;15;1;24;0
WireConnection;51;0;52;0
WireConnection;51;1;15;0
WireConnection;0;0;51;0
ASEEND*/
//CHKSM=96BA9B985BE6B1ACB4F668D554EFB762FDE3B5FB