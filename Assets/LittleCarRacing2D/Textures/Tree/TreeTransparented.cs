using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class TreeTransparented : MonoBehaviour
{
    public Material treeMaterial;
    private void LateUpdate()
    {
        if (treeMaterial) treeMaterial.SetVector("_PlayerPosition", transform.position);
    }
}
