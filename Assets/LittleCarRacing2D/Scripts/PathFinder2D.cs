using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder2D : MonoBehaviour {
    public Transform target;
    List<Point> edge = new List<Point>();
    List<Point> fill = new List<Point>();
    Point finishPoint;
    List<Vector2> traectory = new List<Vector2>();
    bool isFound = false;
    static float grid = 1f;

    public bool find = false;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(find)
        {
            Calc();
            find = false;
        }
        if(isFound)
        {
            Point _p = finishPoint;
            while (_p.steps >= 0)
            {
                traectory.Add(_p.position);
                _p = _p.fromPoint;
            }
            traectory.Reverse();
            isFound = false;
        }
        if(traectory.Count > 1)
        {
            for (int i = 1; i < traectory.Count; i++)
                Debug.DrawLine(traectory[i - 1], traectory[i]);
        }
	}


    void Calc()
    {
        Vector2 targetPos = target.position;
        Vector2 myPos = transform.position;
        float allDistance = Vector2.Distance(targetPos, myPos);
        Point p = new Point(
            myPos, 
            0, 
            Vector2.Distance(myPos, targetPos));
        edge.Add(p);
        for(int i = 0; !isFound && i < 100; i++)
        {
            // Выбрать самую оптимальную из поверхности
            Point fromP = RelevantedPointFromEdge(allDistance);
            fill.Add(fromP);
            edge.Remove(fromP);

            int steps = fromP.steps + 1;

            if (fromP.iCameFromDir != Point.Directions.up)
            {
                Vector2 upPos = fromP.position + Vector2.up * grid;                
                float distance = Vector2.Distance(upPos, targetPos);
                Point.Directions fromDir = Point.Directions.down;
                Point pUp = new Point(upPos, steps, distance, fromP, fromDir);
                edge.Add(pUp);
                if (distance < 1f)
                {
                    finishPoint = pUp;
                    isFound = true;
                }
            }
            if (fromP.iCameFromDir != Point.Directions.upRight)
            {
                Vector2 upRightPos = fromP.position + new Vector2(Mathf.Sqrt(3f)/2f, 0.5f) * grid;                
                float distance = Vector2.Distance(upRightPos, targetPos);
                Point.Directions fromDir = Point.Directions.downLeft;
                Point pUpR = new Point(upRightPos, steps, distance, fromP, fromDir);
                edge.Add(pUpR);
                if (distance < 1f)
                {
                    finishPoint = pUpR;
                    isFound = true;
                }
            }
            if (fromP.iCameFromDir != Point.Directions.downRight)
            {
                Vector2 downRightPos = fromP.position + new Vector2(Mathf.Sqrt(3f) / 2f, -0.5f) * grid;                
                float distance = Vector2.Distance(downRightPos, targetPos);
                Point.Directions fromDir = Point.Directions.upLeft;
                Point pDownR = new Point(downRightPos, steps, distance, fromP, fromDir);
                edge.Add(pDownR);
                if (distance < 1f)
                {
                    finishPoint = pDownR;
                    isFound = true;
                }
            }
            if (fromP.iCameFromDir != Point.Directions.down)
            {
                Vector2 downPos = fromP.position + Vector2.down * grid;                
                float distance = Vector2.Distance(downPos, targetPos);
                Point.Directions fromDir = Point.Directions.up;
                Point pDown = new Point(downPos, steps, distance, fromP, fromDir);
                edge.Add(pDown);
                if (distance < 1f)
                {
                    finishPoint = pDown;
                    isFound = true;
                }
            }
            if (fromP.iCameFromDir != Point.Directions.downLeft)
            {
                Vector2 downLeftPos = fromP.position + new Vector2(-(Mathf.Sqrt(3f) / 2f), -0.5f) * grid;
                float distance = Vector2.Distance(downLeftPos, targetPos);
                Point.Directions fromDir = Point.Directions.upRight;
                Point pDownLeft = new Point(downLeftPos, steps, distance, fromP, fromDir);
                edge.Add(pDownLeft);
                if (distance < 1f)
                {
                    finishPoint = pDownLeft;
                    isFound = true;
                }
            }
            if (fromP.iCameFromDir != Point.Directions.upLeft)
            {
                Vector2 UpLeftPos = fromP.position + new Vector2(-(Mathf.Sqrt(3f) / 2f), 0.5f) * grid;
                float distance = Vector2.Distance(UpLeftPos, targetPos);
                Point.Directions fromDir = Point.Directions.downRight;
                Point pUpLeft = new Point(UpLeftPos, steps, distance, fromP, fromDir);
                edge.Add(pUpLeft);
                if (distance < 1f)
                {
                    finishPoint = pUpLeft;
                    isFound = true;
                }
            }
            if (isFound) Debug.Log("Ну тип я нашел");






        }
    }
    Point RelevantedPointFromEdge(float allDistance)
    {
        Point relPoint = null;
        for (int i = 0; i < edge.Count; i++)
        {
            if(i == 0)
            {
                relPoint = edge[i];
            }
            else if(edge[i].distanceToTarget + edge[i].steps < relPoint.distanceToTarget + relPoint.steps)
            {
                relPoint = edge[i];
            }
        }
        return relPoint;
    }
        
    public class Point
    {
        public Vector2 position;
        public int steps;
        public float distanceToTarget;
        public Point fromPoint;
        public enum Directions {up, upRight, downRight, down, downLeft, upLeft, nope};
        public Directions iCameFromDir;

        public Point (
            Vector2 _pos, 
            int _steps, 
            float _dist, 
            Point _fromPoint = null, 
            Directions _fromDir = Directions.nope)
        {
            position = _pos;
            this.steps = _steps;
            distanceToTarget = _dist;
            fromPoint = _fromPoint;
            iCameFromDir = _fromDir;
        }
    }
}
