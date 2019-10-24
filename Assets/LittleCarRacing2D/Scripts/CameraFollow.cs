using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]
public class CameraFollow : MonoBehaviour
{ 
    [SerializeField] private Transform target;
    [SerializeField] private Vector2 followOffset;
    [SerializeField] private bool useSmoothing;
    [SerializeField, Range(0.1f, 1f)] private float smoothAmount;
    [SerializeField] private bool useRigidbodyVelocityOffset;
    [SerializeField] private float rigidbodyOffsetMaxDistance;
    [SerializeField] private float rigidbodyOffsetInsensitivity;
    [SerializeField] private bool useCameraAdjustSizing;
    [SerializeField] private float cameraMinimumSize;
    [SerializeField, Range(0.1f, 2f)] private float cameraSizingMultiplier;
    [SerializeField] private float cameraSizingSmooth;

    [SerializeField, Space(12)] private bool showCameraTargetLine;

    private Camera thisCamera;
    private Rigidbody2D targetRb;
    private Vector2 cameraVelocity;
    private Vector2 cameraTargetPosition;
    private float cameraSizingVelocity;
    private float cameraZoffset;

    private void Start()
    {
        thisCamera = GetComponent<Camera>();
        targetRb = target.GetComponent<Rigidbody2D>();

        cameraVelocity = Vector2.zero;
        cameraZoffset = transform.position.z;

        if (rigidbodyOffsetInsensitivity <= 0) rigidbodyOffsetInsensitivity = 1f;
    }
    
    void Update()
    {        
        if (useRigidbodyVelocityOffset)
            cameraTargetPosition = (Vector2)target.position + targetRb.velocity.normalized * Mathf.Clamp(targetRb.velocity.magnitude / rigidbodyOffsetInsensitivity * rigidbodyOffsetMaxDistance, 0, rigidbodyOffsetMaxDistance);
        else
            cameraTargetPosition = target.position;

        if (useSmoothing)
        {
            Vector3 newPosition = Vector2.SmoothDamp(transform.position, cameraTargetPosition, ref cameraVelocity, smoothAmount);
            newPosition.z = cameraZoffset;
            transform.position = newPosition;
        }
        else
        {
            Vector3 newPosition = cameraTargetPosition;
            newPosition.z = cameraZoffset;
            transform.position = newPosition;
        }

        if (useCameraAdjustSizing)
            thisCamera.orthographicSize = Mathf.Clamp(Mathf.SmoothDamp(thisCamera.orthographicSize, targetRb.velocity.magnitude * cameraSizingMultiplier, ref cameraSizingVelocity, cameraSizingSmooth), cameraMinimumSize, 100f);
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (Application.isPlaying && showCameraTargetLine)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(target.position, (Vector2)target.position + targetRb.velocity.normalized * Mathf.Clamp(targetRb.velocity.magnitude / rigidbodyOffsetInsensitivity * rigidbodyOffsetMaxDistance, 0, rigidbodyOffsetMaxDistance));
        }
    }
#endif
}
