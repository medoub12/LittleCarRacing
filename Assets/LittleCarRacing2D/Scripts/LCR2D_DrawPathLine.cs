using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class LCR2D_DrawPathLine : MonoBehaviour
{
    bool started = false;
    int chCount;
    public bool showChilds = false;
    bool last_showChilds;



    void Update()
    {
        chCount = transform.childCount;
        if (last_showChilds != showChilds)
        {
            for (int i = 0; i < chCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(showChilds);
            }
            last_showChilds = showChilds;
        }
        if (chCount > 1)
        {
            Vector2 lastPathPos = transform.GetChild(0).position;
            for (int i = 1; i < chCount; i++)
            {
                Vector2 pos = transform.GetChild(i).position;
                Debug.DrawLine(lastPathPos, pos);
                lastPathPos = pos;
            }
            if (chCount > 2)
            {
                Debug.DrawLine(lastPathPos, transform.GetChild(0).position);
            }
        }
    }
}
