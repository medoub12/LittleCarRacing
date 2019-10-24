using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class AICarsController : MonoBehaviour
{
    public PathCreator pathCreator;
    public CarController[] AICars;

    private VertexPath path; 

    private void Start()
    {
        path = pathCreator.path;
    }
    
    private void FixedUpdate()
    {
        for (int i = 0; i < AICars.Length; i++)
        { 
            Vector3 nextPoint = path.GetPointAtDistance(path.GetClosestDistanceAlongPath(AICars[i].transform.position) + AICars[i].AIDistanceFactor);
            float signedAngle = Vector2.SignedAngle(AICars[i].transform.up, nextPoint - AICars[i].transform.position);
            bool invert = false;

            if (Mathf.Abs(signedAngle) < 107)
                AICars[i].Forward();
            else
            {
                AICars[i].Backward();
                invert = true;
            }

            if (signedAngle > 5)
            {
                if (!invert)
                    AICars[i].Left();
                else
                    AICars[i].Right();
            }
            else if (signedAngle < -5)
            {
                if (!invert)
                    AICars[i].Right();
                else
                    AICars[i].Left();
            }
        }
    }
}
