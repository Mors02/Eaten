using UnityEngine;
using System;

public class ConnectManager : MonoBehaviour
{
    private bool _clicking = false;
    private RectTransform _lineTransform, _currectRectTransform;
    private Canvas _canvas;
    [SerializeField]
    private GameObject _line;
    private Vector2 _defaultPosition, _dotStartPosition;
    private int _currentBlock = -1;
    private Vector2 _offset;
    private int[] _selectedBlocks;
    private int _howManyBlocks = 0;
    private int _MAXBLOCKS = 4;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _canvas = GetComponentInParent<Canvas>();
        _selectedBlocks = new int[_MAXBLOCKS];
        _howManyBlocks = 0;
    }

    public void SelectStartingBlock(Transform transform)
    {
        Debug.Log("Clicked on " + gameObject.name);
        _clicking = true;

        // Instantiate line as child of canvas (not this dot)
        GameObject line = Instantiate(_line, transform.position, Quaternion.identity, transform);
        _lineTransform = line.GetComponent<RectTransform>();

        // Set initial position and size
        //_lineTransform.anchoredPosition = _dotRectTransform.anchoredPosition;
        //_lineTransform.sizeDelta = new Vector2(0, _lineTransform.sizeDelta.y);
        _currectRectTransform = transform.gameObject.GetComponent<RectTransform>();

        // Store the dot position in canvas coordinates for consistent calculations
        _dotStartPosition = _currectRectTransform.position;//Input.mousePosition / _canvas.scaleFactor;

        _defaultPosition = new Vector3(this.transform.position.x + (_lineTransform.sizeDelta.x / 2), transform.position.y);
        line.transform.position = this._defaultPosition;
        //Vector2 mousePos = (Input.mousePosition / _canvas.scaleFactor);
        //_offset = _dotStartPosition - _currectRectTransform;
        //Debug.Log(_offset);
        // Set pivot to left (0, 0.5) so line extends from the dot
        _lineTransform.pivot = new Vector2(0, 0.5f);

        _currentBlock = Int32.Parse(transform.gameObject.name);
        Debug.Log("index: " + _howManyBlocks);
        _selectedBlocks[_howManyBlocks++] = _currentBlock;
        Debug.Log(_currentBlock);
    }

    public void StopClicking()
    {
        _clicking = false;
        //check if we didnt already destroy the line when reaching max length
        if (_lineTransform != null)
            Destroy(this._lineTransform.gameObject);
        _currectRectTransform = null;
        _currentBlock = -1;
        _selectedBlocks = new int[_MAXBLOCKS];
        _howManyBlocks = 0;
    }
    
    public bool IsClicking()
    {
        return _clicking;
    }

    public bool CanSelect(string name)
    {
        if (_howManyBlocks >= _MAXBLOCKS)
        {
            StopClicking();
            return false;
        }
            

        int id = Int32.Parse(name);
        for (int i = 0; i<_howManyBlocks; i++)
        {
            if (_selectedBlocks[i] == id)
                return false;
        }
        return true;
    }

    public void Connect(Transform transform)
    {
        // Calculate direction and distance
        Vector2 dotPosition = _currectRectTransform.position;
        Vector2 direction = new Vector2(transform.position.x, transform.position.y) - _dotStartPosition;
        float distance = direction.magnitude;

        // Update line length
        _lineTransform.sizeDelta = new Vector2(distance, _lineTransform.sizeDelta.y);

        // Calculate and apply rotation
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        _lineTransform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        // Keep line positioned at the dot
        _lineTransform.position = dotPosition;

        //add to the selected blocks
        SelectStartingBlock(transform);
    }


    void Update()
    {
        if (_clicking && _lineTransform != null)
        {
            // Convert mouse position to canvas space
            Vector2 mouseCanvasPos;
            /*RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _canvas.GetComponent<RectTransform>(),
                Input.mousePosition,
                _camera,
                out mouseCanvasPos
            );*/
            mouseCanvasPos = Input.mousePosition / _canvas.scaleFactor;

            //mouseCanvasPos = Camera.main.ScreenToViewportPoint(Input.mousePosition);

            
            // Calculate direction and distance
            Vector2 dotPosition = _currectRectTransform.position;
            Vector2 direction = mouseCanvasPos - _dotStartPosition;
            float distance = direction.magnitude;

            // Update line length
            _lineTransform.sizeDelta = new Vector2(distance, _lineTransform.sizeDelta.y);

            // Calculate and apply rotation
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            _lineTransform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            // Keep line positioned at the dot
            _lineTransform.position = dotPosition;
        }
    }


}