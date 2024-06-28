using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TogglePanel : MonoBehaviour
{
    public Button ToggleButton;
    public RectTransform warPanel;
    public float transistionspeed = 5f;
    public float swipeThreshold = 50f;

    public bool isPanelVisible = false;
    private Vector2 hiddenPosition;
    private Vector2 visiblePosition;

    private Vector2 startTouchPosition;
    private bool isDragging = false;


    // Start is called before the first frame update
    void Start()
    {
        float panelWidth = warPanel.rect.width;
        hiddenPosition = new Vector2(-panelWidth, warPanel.anchoredPosition.y);
        visiblePosition = new Vector2(446, warPanel.anchoredPosition.y);

        warPanel.anchoredPosition = hiddenPosition;

        ToggleButton.onClick.AddListener(TogglePanelVisibility);
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 targetPosition = isPanelVisible ? visiblePosition : hiddenPosition;
        warPanel.anchoredPosition = Vector2.Lerp(warPanel.anchoredPosition, targetPosition, Time.deltaTime * transistionspeed);

        DetectSwipeOrDrag();
    }
    void DetectSwipeOrDrag() 
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            startTouchPosition = Input.mousePosition;
            isDragging = true;

        }
        else if (Input.GetMouseButtonUp(0) && isDragging)
        {
            isDragging = false;
            Vector2 endTouchPosition = Input.mousePosition;
            HandleSwipeOrDrag(startTouchPosition, endTouchPosition); 
        }

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {

                startTouchPosition = touch.position;
                isDragging = true;
            }
            else if (touch.phase == TouchPhase.Ended && isDragging)
            {
                isDragging = false;
                Vector2 endtouchPosition = touch.position;
                HandleSwipeOrDrag(startTouchPosition, endtouchPosition);
            }

        }
    }
    void HandleSwipeOrDrag(Vector2 start, Vector2 end)
    {
        Vector2 delta = end - start;

        if (Mathf.Abs(delta.x) > swipeThreshold && Mathf.Abs(delta.y) < swipeThreshold / 2)
        {
            if (delta.x > 0)
            {
                // Swipe or drag from left to right detected
                showPanel();
            }
            else
            {
                // Swipe or drag from right to left detected
                hidePanel();
            }
        }
    }
    void showPanel()
    {
        isPanelVisible = true;
    }
    void hidePanel()
    {

    isPanelVisible = false; 
    
    }

    void TogglePanelVisibility()
    {
        isPanelVisible = !isPanelVisible;
    }
}
