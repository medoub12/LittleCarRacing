using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class G51_SortOrderEditor : MonoBehaviour {
    public float orderInLayer;
    float lastf_orderInLayer;
    int last_orderInLayer;
    SpriteRenderer sprd;
	void Update ()
    {
        if(orderInLayer != lastf_orderInLayer)
        {
            int i = Mathf.RoundToInt(orderInLayer);
            if (i != last_orderInLayer)
            {
                if (sprd)
                {
                    sprd.sortingOrder = i;
                }
                else
                {
                    sprd = GetComponent<SpriteRenderer>();
                    sprd.sortingOrder = i;
                }
                    last_orderInLayer = i;
            }
            lastf_orderInLayer = orderInLayer;
        }
	}


}
