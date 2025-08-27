using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using System;
public class Connect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler, IPointerDownHandler
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

    private void Awake()
    {
        _cm = GameObject.FindGameObjectWithTag("Canvas").GetComponent<ConnectManager>();
        //TODO: should be passed by the gameManager
        int id = Int32.Parse(this.gameObject.name);

        this._cb = GameManager.GetCharacter(id);


        // No character means that this square is not used
        if (this._cb == null)
        {
            _visibleSection.SetActive(false);
            return;
        }

        this._cb.SetId(id);
        this._image.sprite = this._cb.Sprite;

        this._cb._onStatChange.AddListener(UpdateGraphics);
        this._cb._onStatChange.Invoke();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _cm.SelectStartingBlock(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_cm.IsClicking() && _cm.CanSelect(gameObject.name))
        {
            _cm.Connect(this);

        }
        
        GameAssets.i.UiManager.SetupCharacter(this._cb);
        this.ChangeSlider(true);

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        this._cm.StopClicking();
    }

    public Ability GetAbility()
    {
        Debug.Log(_cb.Ability.Name);
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
    }

    public void UpdateGraphics()
    {
        this._smallSlider.value = this._bigSlider.value = this._cb.CurrentHP / this._cb.MaxHP;
        this._text.text = this._cb.CurrentHP.ToString() + "/" + this._cb.MaxHP.ToString();
    }
}