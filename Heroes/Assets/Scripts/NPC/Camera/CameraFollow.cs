using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;  // Reference to the player's transform
    public float smoothSpeed = 0.125f;  // Smoothing factor for the camera movement
    public Vector3 offset;  // Offset from the player position
    public GameObject background;  // Reference to the background GameObject

    private Camera cam;
    private Collider2D backgroundCollider;

    private void Start()
    {
        cam = GetComponent<Camera>();
        backgroundCollider = background.GetComponent<Collider2D>();
    }

    private void LateUpdate()
    {
        if (target != null)
        {
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = ClampPositionWithinBackground(smoothedPosition);
        }
    }

    private Vector3 ClampPositionWithinBackground(Vector3 position)
    {
        if (backgroundCollider != null)
        {
            Bounds bounds = backgroundCollider.bounds;
            float cameraHalfHeight = cam.orthographicSize;
            float cameraHalfWidth = cam.aspect * cameraHalfHeight;

            float minX = bounds.min.x + cameraHalfWidth;
            float maxX = bounds.max.x - cameraHalfWidth;
            float minY = bounds.min.y + cameraHalfHeight;
            float maxY = bounds.max.y - cameraHalfHeight;

            position.x = Mathf.Clamp(position.x, minX, maxX);
            position.y = Mathf.Clamp(position.y, minY, maxY);
        }
        return position;
    }
}
