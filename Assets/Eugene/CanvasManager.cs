using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CanvasManager : MonoBehaviour
{
    public TextMeshProUGUI lapCounter;
    public TextMeshProUGUI distanceCounter;
    public CarRaceStats stats;

    private void LateUpdate()
    {
        lapCounter.text = stats.lap.ToString() + " / 3";
        distanceCounter.text = stats.distanceDriven.ToString();
    }
}
