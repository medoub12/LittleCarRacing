using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public int number;
    public bool isFinishLine;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CarRaceStats stats = collision.GetComponentInParent<CarRaceStats>();
        if (stats)
        {
            if (stats.checkpointNumber + 1 == number)
            {
                if (isFinishLine)
                {
                    stats.IncreaseLap();
                    stats.checkpointNumber = 1;
                }
                else
                    stats.checkpointNumber = number;
            }
            else if (isFinishLine)
            {
                stats.checkpointNumber = 1;
            }
        }
    }
}
