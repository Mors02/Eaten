using UnityEngine;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;

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
    private List<Ability> _abilities;
    [SerializeField]
    private CombatManager _cm;

    [SerializeField]
    private Transform _party;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _canvas = GetComponentInParent<Canvas>();
        _selectedBlocks = new int[_MAXBLOCKS];
        _howManyBlocks = 0;
        _abilities = new List<Ability>();
    }

    public void SelectStartingBlock(Connect connect)
    {
        Transform transform = connect.gameObject.transform;

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

        _selectedBlocks[_howManyBlocks++] = _currentBlock;
        _abilities.Add(connect.GetAbility());
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

    public void Restart()
    {
        this.StopClicking();
        _abilities = new List<Ability>();
        GameObject[] lines = GameObject.FindGameObjectsWithTag("Line");

        foreach (GameObject line in lines)
        {
            Destroy(line);
        }

    }

    public void Confirm()
    {
        if (GameManager.i.CanPlay)
        {
            ActivateAllAbilities();
            Restart();   
        }        
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
        for (int i = 0; i < _howManyBlocks; i++)
        {
            if (_selectedBlocks[i] == id)
                return false;
        }
        return true;
    }

    public void Connect(Connect connect)
    {
        Transform transform = connect.gameObject.transform;
        // Calculate direction and distance
        Vector2 dotPosition = _currectRectTransform.position;
        Vector2 direction = new Vector2(transform.position.x, transform.position.y) - _dotStartPosition;
        float distance = direction.magnitude / _canvas.scaleFactor;

        // Update line length
        _lineTransform.sizeDelta = new Vector2(distance, _lineTransform.sizeDelta.y);

        // Calculate and apply rotation
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        _lineTransform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        // Keep line positioned at the dot
        _lineTransform.position = dotPosition;

        //add to the selected blocks
        SelectStartingBlock(connect);
    }

    public void ActivateAllAbilities()
    {

        _cm.PlayerTurn(_abilities, _party);

        _abilities = new List<Ability>();
    }

    /// <summary>
    /// A bit clunky but removes all the highlights from the party
    /// </summary>
    public void RemoveAllHighlights()
    {
        this._cm.RemoveAllHighlights();
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
            mouseCanvasPos = Input.mousePosition; /// _canvas.scaleFactor;

            //mouseCanvasPos = Camera.main.ScreenToViewportPoint(Input.mousePosition);


            // Calculate direction and distance
            Vector2 dotPosition = _currectRectTransform.position;
            Vector2 direction = (mouseCanvasPos - _dotStartPosition) / _canvas.scaleFactor;
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