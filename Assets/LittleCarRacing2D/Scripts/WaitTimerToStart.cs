using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitTimerToStart : MonoBehaviour {

    public List<GameObject> Cars;
    public float WaitTime;
    float StartTime;
    int Leng;
    GameObject Player;
    GameObject Timer;
    bool StartALL_complete = false;

    void Start () {
        Leng = Cars.Count;
        Player = gameObject.GetComponent<LCR2D_InputManager>().controlledCar.gameObject;
        Timer = GameObject.Find("Canvas").transform.Find("TIMER").gameObject;
        StopAll();
		
	}
	
	// Update is called once per frame
	void Update () {
        if (StartTime != 0 && Time.time > StartTime && StartALL_complete != true) StartAll();
	}
    void StartAll()
    {
        for (int i = 0; i < Leng; i++)
        {
            if (Cars[i] != Player)
            {
                Cars[i].GetComponent<LCR2D_AI>().enabled = true;
                Cars[i].GetComponent<LCR2D_CarBehavior2D>().MotorForce = Random.Range(20, 100);
                Cars[i].GetComponent<LCR2D_CarBehavior2D>().TurnForce = Random.Range(15, 75);
            }
            Cars[i].GetComponent<LCR2D_CarBehavior2D>().enabled = true;
            Timer.SetActive(false);
            StartALL_complete = true;
        }
    }
    void StopAll()
    {
        for (int i = 0; i < Leng; i++)
        {
            Cars[i].GetComponent<LCR2D_AI>().enabled = false;
            Cars[i].GetComponent<LCR2D_CarBehavior2D>().enabled = false;


        }
    }
    public void StartTimer()
    {
        StartTime = Time.time + WaitTime;
    }
}
