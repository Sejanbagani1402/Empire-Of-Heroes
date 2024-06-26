using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public float zoomSpeed = 0.5f;  // Speed of zoom
    public float minZoom = 5f;  // Minimum zoom level
    public float defaultMaxZoom = 30f;  // Default maximum zoom level
    public float reducedMaxZoom = 21f;  // Reduced maximum zoom level
    public float boundaryBuffer = 10f;  // Distance from boundary to start reducing zoom

    private Camera cam;
    private GameObject player;
    private float currentMaxZoom;

    private void Start()
    {
        cam = GetComponent<Camera>();
        player = GameObject.FindGameObjectWithTag("Player");
        currentMaxZoom = defaultMaxZoom;
    }

    private void Update()
    {
        UpdateMaxZoomBasedOnPlayerPosition();

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
            cam.orthographicSize = Mathf.Clamp(cam.orthographicSize - scroll * zoomSpeed, minZoom, currentMaxZoom);
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

            cam.orthographicSize = Mathf.Clamp(cam.orthographicSize + deltaMagnitudeDiff * zoomSpeed * Time.deltaTime, minZoom, currentMaxZoom);
        }
    }

    private void UpdateMaxZoomBasedOnPlayerPosition()
    {
        if (player != null)
        {
            Vector3 playerPosition = player.transform.position;

            bool isNearHorizontalBoundary = Mathf.Abs(playerPosition.x) > (130 - boundaryBuffer);
            bool isNearVerticalBoundary = Mathf.Abs(playerPosition.y) > (92 - boundaryBuffer);

            if (isNearHorizontalBoundary || isNearVerticalBoundary)
            {
                currentMaxZoom = reducedMaxZoom;
            }
            else
            {
                currentMaxZoom = defaultMaxZoom;
            }
        }
    }
}
