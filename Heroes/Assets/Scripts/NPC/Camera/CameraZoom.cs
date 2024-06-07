using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public float zoomSpeed = 0.5f;  // Speed of zoom
    public float minZoom = 5f;  // Minimum zoom level
    public float maxZoom = 15f;  // Maximum zoom level

    private Camera cam;

    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    private void Update()
    {
        if (Application.isMobilePlatform)
        {
            HandleMobileZoom();
        }
        else
        {
            HandlePCZoom();
        }
    }

    private void HandlePCZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0.0f)
        {
            cam.orthographicSize = Mathf.Clamp(cam.orthographicSize - scroll * zoomSpeed, minZoom, maxZoom);
        }
    }

    private void HandleMobileZoom()
    {
        if (Input.touchCount == 2)
        {
            Touch touch1 = Input.GetTouch(0);
            Touch touch2 = Input.GetTouch(1);

            Vector2 touch1PrevPos = touch1.position - touch1.deltaPosition;
            Vector2 touch2PrevPos = touch2.position - touch2.deltaPosition;

            float prevTouchDeltaMag = (touch1PrevPos - touch2PrevPos).magnitude;
            float touchDeltaMag = (touch1.position - touch2.position).magnitude;

            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            cam.orthographicSize = Mathf.Clamp(cam.orthographicSize + deltaMagnitudeDiff * zoomSpeed * Time.deltaTime, minZoom, maxZoom);
        }
    }
}
