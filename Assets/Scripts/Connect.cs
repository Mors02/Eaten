using UnityEngine;
using UnityEngine.EventSystems;

public class Connect : MonoBehaviour, IPointerEnterHandler, IPointerUpHandler, IPointerDownHandler
{
    private bool _clicking = false;
    [SerializeField]
    private GameObject _line;
    private Canvas _canvas;
    private RectTransform _lineTransform;
    private RectTransform _dotRectTransform;
    private Camera _camera;

    private void Start()
    {
        _canvas = GetComponentInParent<Canvas>();
        _dotRectTransform = GetComponent<RectTransform>();
        _camera = _canvas.worldCamera ?? Camera.main;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Clicked on " + gameObject.name);
        _clicking = true;

        // Instantiate line as child of canvas (not this dot)
        GameObject line = Instantiate(_line, _canvas.transform);
        _lineTransform = line.GetComponent<RectTransform>();

        // Set initial position and size
        _lineTransform.anchoredPosition = _dotRectTransform.anchoredPosition;
        _lineTransform.sizeDelta = new Vector2(0, _lineTransform.sizeDelta.y);

        // Set pivot to left (0, 0.5) so line extends from the dot
        _lineTransform.pivot = new Vector2(0, 0.5f);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Handle dot connection logic here if needed
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("Stopped clicking");
        _clicking = false;
    }

    void Update()
    {
        if (_clicking && _lineTransform != null)
        {
            // Convert mouse position to canvas space
            Vector2 mouseCanvasPos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _canvas.GetComponent<RectTransform>(),
                Input.mousePosition,
                _camera,
                out mouseCanvasPos
            );

            // Calculate direction and distance
            Vector2 dotPosition = _dotRectTransform.anchoredPosition;
            Vector2 direction = mouseCanvasPos - dotPosition;
            float distance = direction.magnitude;

            // Update line length
            _lineTransform.sizeDelta = new Vector2(distance, _lineTransform.sizeDelta.y);

            // Calculate and apply rotation
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            _lineTransform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            // Keep line positioned at the dot
            _lineTransform.anchoredPosition = dotPosition;
        }
    }
}