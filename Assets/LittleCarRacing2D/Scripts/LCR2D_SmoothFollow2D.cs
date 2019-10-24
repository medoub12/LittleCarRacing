
using UnityEngine;
using System.Collections;

public class LCR2D_SmoothFollow2D : MonoBehaviour {


    public Transform target ;
    float  smoothTime = 0.1f;
    float offset, AddSize, SmoothDampPos = 0;
    Vector2 targetPos,  targetVel, myPos, actualPos;

    public bool useSmoothing, addForward = false;


    void Start()

    {
        targetVel = Vector2.zero;
    }
    
    void Update()

    {        
        actualPos = Vector2.zero;
        myPos = transform.position;

        if(addForward)
        {
            Vector2 vel = target.GetComponent<Rigidbody2D>().velocity;
            targetPos = (Vector2)target.position + vel;
        }
        else
        {
            targetPos = target.position;
        }        


        if (useSmoothing)

        {

            actualPos.x =  Mathf.SmoothDamp( myPos.x, targetPos.x, ref targetVel.x, smoothTime);
            actualPos.y= Mathf.SmoothDamp( myPos.y, targetPos.y + offset, ref targetVel.y, smoothTime);
            GetComponent<Camera>().orthographicSize = 8f + (AddSize / 4);

        }

        else

        {
            actualPos.x = targetPos.x;
            actualPos.y = targetPos.y + offset;
        }

        Vector3 newPos = new Vector3(actualPos.x, actualPos.y , transform.position.z);
        transform.position = Vector3.Slerp(transform.position, newPos, Time.time);
        AddSize = Mathf.SmoothDamp(AddSize, target.GetComponent<Rigidbody2D>().velocity.magnitude, ref SmoothDampPos, 1f);
    }

}
