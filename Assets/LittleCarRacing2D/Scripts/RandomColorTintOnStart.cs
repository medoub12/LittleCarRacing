using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomColorTintOnStart : MonoBehaviour
{
    private SpriteRenderer spr;
    // Start is called before the first frame update
    void Start()
    {
        spr = GetComponent<SpriteRenderer>();
        spr.color = new Color(Random.Range(1f, 0.55f), Random.Range(1f, 0.55f), Random.Range(1f, 0.55f), 1f);
    }
}
