using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BarCharacter : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField]
    private bool _static;

    private CharacterBrain _character;

    public CharacterBrain Character => _character;

    [SerializeField]
    private Image _image;

    [SerializeField]
    private Animator _animator;

    private MenuManager _mm;

    [Range(0, 100)]
    [SerializeField]
    private int _spawnProbability;

    public void OnPointerEnter(PointerEventData eventData)
    {
        try
        {
            GameAssets.i.UiManager.SetupCharacter(this._character, !_static);
            ResetTriggers();
            _animator.SetTrigger("Zoom");
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
        ;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        try
        {
            ResetTriggers();
            _animator.SetTrigger("ZoomOut");
        }
        catch (Exception e)
        {

        }
        ;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this._mm = GameObject.FindGameObjectWithTag("Canvas").GetComponent<MenuManager>();
        if (!_static)
        {
            int id = Int32.Parse(gameObject.name);
            this._character = GameManager.GetCharacter(id);

            // if the character is null means that the party is smaller than the maximum, remove the graphics.
            if (this._character == null)
            {
                this.gameObject.SetActive(false);
            }
            else
            {
                this._image.sprite = _character.Sprite;
            }
        }
        else if (UnityEngine.Random.Range(0, 100f) < _spawnProbability)
        {
            //if they spawn, randomize them
            this._character = GameAssets.RandomCharacter();
            this._image.sprite = _character.Sprite;
            this._character.Hunger = 0;

            //set the listener on hunger changes.
            this._character._onStatChange.AddListener(Hire);
        }
        else
        {
            //if they are not spawned, then remove
            this.gameObject.SetActive(false);
        }
    }

    public void ResetTriggers()
    {
        _animator.ResetTrigger("Zoom");
        _animator.ResetTrigger("ZoomOut");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        this._mm.SwitchPartyVisual();
    }

    private void Hire()
    {
        //if the hunger meter is full
        if (_character.Hunger >= 100)
        {
            //the character is added to the party
            GameManager.AddCharacter(this._character);
            this.gameObject.SetActive(false);
        }
    }
}
