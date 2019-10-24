using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LCR2D_GoToURL : MonoBehaviour
{
    public string url;
    public void OpenUrl()
    {
         //Application.OpenURL(url);
        Application.ExternalEval("window.open(\""+ url +"\",\"_blank\")");
    }
}
