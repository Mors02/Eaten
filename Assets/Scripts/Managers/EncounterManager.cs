using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EncounterManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _buttons;

    [SerializeField]
    private GameObject _eventSection, _resultSection;

    [SerializeField]
    private TMP_Text _title, _description, _result;

    [SerializeField]
    private Animator _animator;

    private EventSO _event;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this._event = GameManager.i.SelectedEvent;

        this._title.text = _event.title;

        this._description.text = _event.description;
        int i = 0;
        ///setup the options available
        for (; i < _event.options.Count(); i++)
        {
            TMP_Text text = _buttons[i].GetComponentInChildren<TMP_Text>();
            text.text = _event.options[i];
            if (_event.CheckOptionAvailability(i))
            {
                int index = i;
                _buttons[i].GetComponent<Button>().onClick.AddListener(delegate { _event.EncounterOptions(index); });
                _buttons[i].GetComponent<Button>().onClick.AddListener(delegate { Continue(index); });
            }
            else
            {
                text.text = "[Blocked] " + text.text;
                _buttons[i].GetComponent<Button>().interactable = false;
            }
        }

        ///continue with the empty buttons and remove them
        for (; i < _buttons.Length; i++)
        {
            _buttons[i].gameObject.SetActive(false);
        }
    }

    public void Continue(int index)
    {
        _eventSection.SetActive(false);
        _result.text = _event.results[index];
        _resultSection.SetActive(true);
    }

    public void Finish()
    {
        _animator.SetTrigger("Exit");
        Debug.Log(_animator.GetCurrentAnimatorStateInfo(0).length);
        Invoke("ChangeScene", _animator.GetCurrentAnimatorStateInfo(0).length);
    }

    public void ChangeScene() {

        SceneManager.LoadScene("PartyScene");
    }

}
