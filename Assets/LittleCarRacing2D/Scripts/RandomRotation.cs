using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomRotation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
              transform.rotation = quaternion.Euler(0f,0f,Random.Range(0f,360f));
    }
}
