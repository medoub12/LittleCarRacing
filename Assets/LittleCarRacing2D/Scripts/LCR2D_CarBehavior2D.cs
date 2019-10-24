using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LCR2D_CarBehavior2D : MonoBehaviour
{
    public float 
        MotorForce, 
        TurnForce;

    Rigidbody2D 
        R2D;
    int 
        Direction = 0;
    float 
        MotorForceBySpeed, 
        TurnForceBySpeed, 
        TurnByVel;
    Animator 
        WheelTurnAnim, 
        WheelRotateAnim;
    Vector2 
        MyPos, 
        MyUp, 
        MyRight;

    void Start()
    {
        R2D = GetComponent<Rigidbody2D>();
        WheelTurnAnim = 
            transform.Find("Wheels").
            transform.Find("Top").
            GetComponent<Animator>();
        WheelRotateAnim = 
            transform.
            Find("Wheels").
            GetComponent<Animator>();

        if (MotorForce == 0)
            MotorForce  = 40;
        if (TurnForce == 0)
            TurnForce   = 15;
    }
    void Update()
    {
        Vector2 Dir     = transform.InverseTransformVector(R2D.velocity);
        MyPos           =   transform.position;
        MyUp            =   transform.up;
        MyRight         =   transform.right;

        if (Dir.magnitude < 2)
        {
            TurnByVel = Dir.magnitude / 2;
        }
        else
        {
            TurnByVel = 1;
        }

        if (Dir.y > 0f)
        {
            Direction = 1;
        }
        else
        {
            Direction = -1;
        }
        if (Dir.magnitude >= 0 && Dir.magnitude <= 1)
        {
            MotorForceBySpeed = MotorForce * 250;
            TurnForceBySpeed = TurnForce * 100;
        }
        if (Dir.magnitude > 1)
        {
            MotorForceBySpeed = MotorForce * 1000;
            TurnForceBySpeed = TurnForce * 250;
        }

        if (Dir.magnitude > 0.2f)
        {
            WheelRotateAnim.SetTrigger("Move");
        }
        else
        {
            WheelRotateAnim.SetTrigger("Stop");
        }
    }

    public void Forward()
    {
        R2D.AddForce(MyUp * MotorForceBySpeed);
        Debug.DrawRay(MyPos, MyUp * 2, Color.green);
    }
    public void Backward()
    {
        R2D.AddForce(-MyUp * MotorForceBySpeed / 2);
        Debug.DrawRay(MyPos, -MyUp, Color.green);
    }
    public void Left()
    {
        R2D.AddTorque(TurnForceBySpeed * Direction * TurnByVel);
        WheelTurnAnim.SetTrigger("Left");
        Debug.DrawRay(MyPos, new Vector2((-MyRight + MyUp).normalized.x * 2, (-MyRight + MyUp).normalized.y * 2), Color.green);
    }
    public void Right()
    {
        R2D.AddTorque(-TurnForceBySpeed * Direction * TurnByVel);
        WheelTurnAnim.SetTrigger("Right");
        Debug.DrawRay(MyPos, new Vector2((MyRight + MyUp).normalized.x * 2, (MyRight + MyUp).normalized.y * 2), Color.green);
    }
}
