using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LCR2D_InputManager : MonoBehaviour
{
    public CarController controlledCar;
    public PathCreation.PathCreator path;
    private void FixedUpdate()
    {
        if (controlledCar != null)
        {
            if (Input.GetKey(KeyCode.W))
            {
                controlledCar.Forward();
            }
            if (Input.GetKey(KeyCode.S))
            {
                controlledCar.Backward();
            }
            if (Input.GetKey(KeyCode.A))
            {
                controlledCar.Left();
            }
            if (Input.GetKey(KeyCode.D))
            {
                controlledCar.Right();
            }
        }
    }
}
