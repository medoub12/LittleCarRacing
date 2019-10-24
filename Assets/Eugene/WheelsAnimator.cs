using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelsAnimator : MonoBehaviour
{
    public Animator rotationAnimator;
    public Transform topWheelLeft;
    public Transform topWheelRight;

    public void SetMoving(bool isMoving)
    {
        rotationAnimator.SetBool("Moving", isMoving);
    }

    public void SetWheelsRotation(float magnitude)
    {
        Quaternion rotation = Quaternion.Euler(0, 0, magnitude);
        topWheelLeft.localRotation = rotation;
        topWheelRight.localRotation = rotation;
    }
}