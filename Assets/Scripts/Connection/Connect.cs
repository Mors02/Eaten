using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using System;
using Unity.VisualScripting;
using System.Collections.Generic;
using System.Linq;
public class Connect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler, IPointerDownHandler, IPointerClickHandler
{
    [SerializeField]
    private GameObject _line;
    private ConnectManager _cm;
    private CharacterBrain _cb;

    [SerializeField]
    private Image _image;

    [SerializeField]
    private Slider _smallSlider;

    [SerializeField]
    private Slider _bigSlider;

    [SerializeField]
    private TMP_Text _text;

    public CharacterBrain Character => _cb;

    [SerializeField]
    private GameObject _visibleSection;

    [SerializeField]
    private Transform _statusContainer;

    [SerializeField]
    private GameObject _border;

    [SerializeField]
    private Image _background;

    [SerializeField]
    private Color _fullHunger, _emptyHunger;

    [SerializeField]
    private bool _connectionActive;

    [SerializeField]
    private Animator _animator;

    private bool _active;

    private void Awake()
    {
        if (_connectionActive)
            _cm = GameObject.FindGameObjectWithTag("Canvas").GetComponent<ConnectManager>();
        //TODO: should be passed by the gameManager
        int id = Int32.Parse(this.gameObject.name);

        this._cb = GameManager.GetCharacter(id);


        // No character means that this square is not used
        if (this._cb == null)
        {
            _active = false;
            _visibleSection.SetActive(false);
            return;
        }

        this._cb.SetId(id);
        this._image.sprite = this._cb.Sprite;

        this._cb._onStatChange.AddListener(UpdateGraphics);
        this._cb._onStatChange.Invoke();

        this._cb._onStatusChange.AddListener(SetupStatuses);
        _active = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //We want to avoid connecting when hovering the line clone
        List<GameObject> hovered = eventData.hovered;
        hovered = hovered.Where(item =>{ Debug.Log(item.name);  return item.name == "Visible"; }).ToList();
        if (hovered.Count == 0)
            return;

        if (_connectionActive)
        {
            _cm.Restart();
            _cm.SelectStartingBlock(this);
        }
            
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //We want to avoid connecting when hovering the line clone
        List<GameObject> hovered = eventData.hovered;
        hovered = hovered.Where(item =>{ Debug.Log(item.name);  return item.name == "Visible"; }).ToList();
        if (hovered.Count == 0)
            return;

        //else connect
        if (_connectionActive && _cm.IsClicking() && _cm.CanSelect(gameObject.name))
        {
            _cm.Connect(this);
        }

        GameAssets.i.UiManager.SetupCharacter(this._cb);
        ResetTriggers();
        _animator.SetTrigger("Enter");
        this.ChangeSlider(true);

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (_connectionActive)
            this._cm.StopClicking();
    }

    public Ability GetAbility()
    {
        return this._cb.Ability;
    }

    private void ChangeSlider(bool big)
    {
        this._bigSlider.gameObject.SetActive(big);
        this._smallSlider.gameObject.SetActive(!big);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        this.ChangeSlider(false);
        ResetTriggers();
        _animator.SetTrigger("Exit");
    }

    public void UpdateGraphics()
    {
        this._smallSlider.value = this._bigSlider.value = (float)this._cb.CurrentHP / this._cb.MaxHP;
        this._text.text = this._cb.CurrentHP.ToString() + "/" + this._cb.MaxHP.ToString();
        this._background.color = Color.Lerp(_emptyHunger, _fullHunger, (float)this._cb.Hunger / 100);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //check if the enemy is low health
        if (!GameManager.i.CanEat)
            return;

        this._cm.RemoveAllHighlights();
        int index = Int32.Parse(this.gameObject.name);
        if (GameManager.i.SelectedCharacter == index)
        {
            GameManager.i.SelectedCharacter = -1;
            return;
        }
        GameManager.i.SelectedCharacter = index;
        this.Highlight(true);
    }

    public void Highlight(bool highlight)
    {
        this._border.SetActive(highlight);
    }

    public void SetupStatuses()
    {
        foreach (Transform child in _statusContainer)
        {
            Destroy(child.gameObject);
        }

        foreach (Status status in _cb.GetStatuses())
        {
            GameObject prefab = Instantiate(GameAssets.i.StatusCharacterPrefab, _statusContainer);
            prefab.GetComponent<Image>().sprite = status.Info.Sprite;
        }
    }

    public void ResetTriggers()
    {
        _animator.ResetTrigger("Exit");
        _animator.ResetTrigger("Enter");
    }

    public bool IsActive()
    {
        return _active;
    }
}