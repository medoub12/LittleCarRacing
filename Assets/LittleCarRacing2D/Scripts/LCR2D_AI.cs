using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LCR2D_AI : MonoBehaviour {

    public Transform MyTarget, PathContainer;
    public bool UsePaths = false;

    Vector2 Direction, MyPos, MyTargetPos;
    LCR2D_CarBehavior2D Br2D;

    int TargetCounter, PathCount = 0;
    bool IHavePaths = false;

    [HideInInspector]   public List<Transform> Paths;
    [HideInInspector]   public bool IcrashFront, IcrashBack = false;
    [HideInInspector]   public int TurnDirection;


	void Start ()
    {
        Br2D = transform.GetComponent<LCR2D_CarBehavior2D>();
    }
	
	void Update ()
    {
        if (MyTarget != null)
        {
            if (IcrashFront == false)
            {
                TypicalBehavior();
            }
            else
            {
                ICrashed();
            }
        }

        if (UsePaths == true)
        {
            if (IHavePaths == true)
            {
                MyTargetDistanceTest();
            }
            else
            {
                GetPathList();
            }
        }
        else
        {
            Paths.Clear();
            IHavePaths = false;
            TargetCounter = 0;
            PathCount = 0;
            MyTarget = null;
            print("Cleared.");
        }
    }

    void GetPathList()
    {
        PathCount = PathContainer.childCount;
        for (int i = 0; i < PathCount; i++) { Paths.Add(PathContainer.GetChild(i));  }
        MyTarget = Paths[TargetCounter];
        IHavePaths = true;
    }
    void MoveToMytarget()
    {
        if (Direction.normalized.y > 0)
        {
            Br2D.Forward();
            if (Direction.normalized.x > 0) { Br2D.Right(); }
            if (Direction.normalized.x < 0) { Br2D.Left(); }
        }
        if (Direction.normalized.y < 0)
        {
            Br2D.Backward();
            if (Direction.normalized.x > 0) { Br2D.Left(); }
            if (Direction.normalized.x < 0) { Br2D.Right(); }
        }
    }
    void MyTargetDistanceTest()
    {
        if ((Vector2.Distance(MyPos, MyTargetPos) < MyTarget.localScale.x))
        {
            if (TargetCounter < PathCount - 1 )
            {
                TargetCounter += 1;
                MyTarget = Paths[TargetCounter];
            }
            else
            {
                TargetCounter = 0;
                MyTarget = Paths[TargetCounter];
            }
        }
    }
    void TypicalBehavior()
    {
        MyTargetPos = MyTarget.position;
        MyPos = transform.position;
        Direction = MyTargetPos - MyPos;
        Debug.DrawRay(MyPos, Direction);
        Direction = transform.InverseTransformDirection(Direction);
        MoveToMytarget();
    }
    public void ICrashed()
    {
        if (TurnDirection == 1)
        {
            Br2D.Backward();
            if (Direction.normalized.x > 0) { Br2D.Left(); }
            if (Direction.normalized.x < 0) { Br2D.Right(); }
        }
        else
        {
            Br2D.Forward();
            if (Direction.normalized.x < 0) { Br2D.Left(); }
            if (Direction.normalized.x > 0) { Br2D.Right(); }
        }

    }
    public void StartCrashTimer()
    {
        StartCoroutine(CrashTimer());
    }
    

    public IEnumerator CrashTimer()
    {
        yield return new WaitForSeconds(0.75f);
        IcrashFront = false;
    }
}
