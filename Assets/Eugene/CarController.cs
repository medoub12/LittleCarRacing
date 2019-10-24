using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class CarController : MonoBehaviour
{
    public float motorPower;
    public float turnPower;
    //[Range(0.1f, 1f)] public float startPower; << NOT IMPLEMENTED
    [Range(0.1f, 1f)] public float backwardsSpeed;
    public float AIDistanceFactor;

    private Rigidbody2D rb;
    private float turnDirection;
    private float currentMotorPower;
    private float currentTurnPower;
    private float turnMultiplier;
    private WheelsAnimator wheelsAnimator;
    private Vector2 velocityVector;
    private float wheelsRotation;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        wheelsAnimator = GetComponentInChildren<WheelsAnimator>();

        currentMotorPower = motorPower * 50000;
        currentTurnPower = turnPower * 12500;
    }

    private void FixedUpdate()
    {
        velocityVector = transform.InverseTransformVector(rb.velocity);

        turnMultiplier = Mathf.Clamp01(velocityVector.magnitude / 2);
        turnDirection = Mathf.Sign(velocityVector.y);

        wheelsAnimator.SetMoving(velocityVector.magnitude > 0.25f);
        wheelsRotation += -Mathf.Sign(wheelsRotation) * Time.fixedDeltaTime * 75;
        wheelsAnimator.SetWheelsRotation(wheelsRotation);
    }

    public void Forward()
    {
        rb.AddForce(transform.up * currentMotorPower * Time.fixedDeltaTime);
    }
    public void Backward()
    {
        rb.AddForce(-transform.up * currentMotorPower / (1 / backwardsSpeed) * Time.fixedDeltaTime);
    }
    public void Left()
    {
        wheelsRotation = Mathf.Clamp(wheelsRotation + Time.fixedDeltaTime * 150, -20, 20);
        rb.AddTorque(currentTurnPower * turnDirection * turnMultiplier * Time.fixedDeltaTime);
    }
    public void Right()
    {
        wheelsRotation = Mathf.Clamp(wheelsRotation - Time.fixedDeltaTime * 150, -20, 20);
        rb.AddTorque(-currentTurnPower * turnDirection * turnMultiplier * Time.fixedDeltaTime);
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        Start();
    }
#endif
}
