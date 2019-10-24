using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LCR2D_CrushChecker : MonoBehaviour {
    LCR2D_AI lcr2d_ai;
    Transform father;
    void Start()
    {
        lcr2d_ai = transform.parent.GetComponent<LCR2D_AI>();
        father = transform.parent;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (lcr2d_ai)
        {
            Vector2 otherPos = other.transform.position;
            Vector2 V2 = father.InverseTransformPoint(new Vector2(otherPos.x, otherPos.y));

            lcr2d_ai.IcrashFront = true;
            if (V2.y >= 1) lcr2d_ai.TurnDirection = 1;
            else lcr2d_ai.TurnDirection = -1;
            lcr2d_ai.StartCrashTimer();
        }
    }
}
