using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarRaceStats : MonoBehaviour
{
    public int checkpointNumber;
    public int lap;
    public float distanceDriven;

    public void IncreaseLap()
    {
        lap++;
    }
}
