using UnityEngine;
using UnityEngine.Events;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _partyModal, _mapModal, _partyGraphics;


    [SerializeField]
    private GameObject _gameSection, _titleSection;

    [SerializeField]
    private Animator _animator;

    [SerializeField]
    private AnimationClip _showAnimation;

    public UnityEvent partyChanged;

    public CharacterBrain CharacterEaten;

    [SerializeField]
    private GameObject _undoButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        partyChanged = new UnityEvent();
        this._partyModal.SetActive(false);
        GameManager.i.CanPlay = true;
        CharacterEaten = null;
        if (GameManager.i.Title)
        {
            _gameSection.SetActive(false);
            _titleSection.SetActive(true);
        }
        else
        {
            this._mapModal.SetActive(false);
            this._partyGraphics.SetActive(true);
            _gameSection.SetActive(false);
            _titleSection.SetActive(false);
        }

    }

    public void StartGame()
    {
        this._titleSection.SetActive(false);
        GameManager.i.Title = false;
        _animator.SetTrigger("Zoom");
        float time = _animator.GetCurrentAnimatorStateInfo(0).length;
        Debug.Log("Animation duration: " + time);
        Invoke("SwitchMapVisual", _showAnimation.length);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void Release()
    {
        GameManager.i.Characters.Remove(GameManager.i.OverlayedCharacter);
        partyChanged.Invoke();
    }

    public void StartEating()
    {
        this.CharacterEaten = GameManager.i.OverlayedCharacter;
        this._undoButton.SetActive(true);

    }

    public void EatPartyMember()
    {
        GameManager.i.Characters.Remove(CharacterEaten);
        partyChanged.Invoke();
        CharacterEaten = null;
        this._undoButton.SetActive(false);
    }

    public void SwitchPartyVisual()
    {
        this._partyModal.SetActive(!this._partyModal.activeSelf);
        this._mapModal.SetActive(false);
    }

    public void AnimateMapVisual()
    {
        this._partyModal.SetActive(false);

        if (this._mapModal.activeSelf)
        {
            _animator.SetTrigger("Zoom");
            float time = _animator.GetCurrentAnimatorStateInfo(0).length;
            Invoke("SwitchMapVisual", _showAnimation.length);
        }
        else
        {
            SwitchMapVisual();
            _animator.SetTrigger("Open");
        }


    }

    public void SwitchMapVisual()
    {
        this._mapModal.SetActive(!this._mapModal.activeSelf);
        this._gameSection.SetActive(true);
    }

}
