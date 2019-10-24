using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class MassPointController : MonoBehaviour
{
    public bool changeMassPointMode;

    private Transform carContainer;
    private Vector3 lastCarContainerPos;
    private bool savedPosition;

    private void Start()
    {
        if (Application.isPlaying) Destroy(this);
    }

    private void LateUpdate()
    {
        if (!carContainer) carContainer = transform.GetChild(0);
        if (carContainer)
        {
            if (changeMassPointMode)
            {
                if (!savedPosition)
                {
                    savedPosition = true;
                    lastCarContainerPos = carContainer.position;
                }
                carContainer.position = lastCarContainerPos;
            }
            else
                savedPosition = false;
        }
    }
}