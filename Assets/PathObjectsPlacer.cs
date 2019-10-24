using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

[RequireComponent(typeof(PathCreator)), ExecuteInEditMode]
public class PathObjectsPlacer : MonoBehaviour
{
    [SerializeField] private Transform container;
    [SerializeField] private GameObject prefab;
    [SerializeField] private float distance = 1f;
    [SerializeField] private bool rotate = true;

    private PathCreator pathCreator;
    private List<GameObject> obstacles;

    private void ClearObstacles()
    {
        foreach (GameObject item in obstacles)
            DestroyImmediate(item);
        obstacles = new List<GameObject>();
    }

    private void OnValidate()
    {
        if (obstacles == null) obstacles = new List<GameObject>();
        if (pathCreator == null) pathCreator = GetComponent<PathCreator>();
        if (prefab != null)
        {
            ClearObstacles();
            if (container == null) container = transform;
            VertexPath path = pathCreator.path;

            if (distance >= 0.5f)
            {
                for (float i = 0f; i < path.length; i += distance)
                {
                    obstacles.Add(Instantiate(prefab, path.GetPointAtDistance(i), rotate ? path.GetRotationAtDistance(i) : Quaternion.identity, container));
                }
            }
        }
    }
}
