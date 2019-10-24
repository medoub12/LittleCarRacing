using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class G51_RockColorizer : MonoBehaviour {    
    public SpriteRenderer fill;
    public SpriteRenderer decor;
    public SpriteRenderer outline;
	void Start ()
    {
		
	}
    public void SetColorFill(Color fillColor)
    {
        if(fill)
        fill.color = fillColor;
    }
    public void SetColorDecor(Color decorColor)
    {
        if(decor)
        decor.color = decorColor;
    }
    public void SetColorOutline(Color outlineColor)
    {
        if(outline)
        outline.color = outlineColor;
    }
    
}
