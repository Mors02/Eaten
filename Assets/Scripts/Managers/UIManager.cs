using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _characterSection, _infosection, _itemSection;

    [SerializeField]
    private TMP_Text _strength, _intelligence, _dexterity, _hunger, _infobox, _name, _abilityDescription, _health;

    [SerializeField]
    private Queue<Message> _messages;

    private float _currentTime;

    private Message _currentMessage;

    private void Start()
    {
        this._messages = new Queue<Message>();
    }

    /// <summary>
    /// Displays the info of a character on the info box
    /// </summary>
    /// <param name="cb">the character information to display</param>
    public void SetupCharacter(CharacterBrain cb)
    {
        this._characterSection.SetActive(true);
        this._infosection.SetActive(false);
        this._strength.text = cb.Strength.ToString();
        this._dexterity.text = cb.Dexterity.ToString();
        this._intelligence.text = cb.Intelligence.ToString();
        this._hunger.text = cb.Hunger.ToString();
        this._hunger.gameObject.SetActive(true);
        this._abilityDescription.text = cb.Ability.Description;
        this._name.text = cb.characterName;
        this._health.text = cb.CurrentHP + "/" + cb.MaxHP;
    }

    public void SetupItem()
    {
        
    }


    /// <summary>
    /// displays the info of the enemy on the info box
    /// </summary>
    /// <param name="ep">the enemy informations</param>
    public void SetupEnemy(EnemyParty ep)
    {
        this._characterSection.SetActive(true);
        this._infosection.SetActive(false);
        this._strength.text = ep.Strength.ToString();
        this._dexterity.text = ep.Dexterity.ToString();
        this._intelligence.text = ep.Intelligence.ToString();
        this._hunger.gameObject.SetActive(false);
        this._abilityDescription.text = ep.Description;
        this._name.text = ep.Name;
        this._health.text = ep.CurrentHP + "/" + ep.MaxHP;
    }

    /// <summary>
    /// clears the textbox
    /// </summary>
    public void Clean()
    {
        this._infosection.SetActive(false);
        this._characterSection.SetActive(false);
    }

    /// <summary>
    /// displays a simple text to be rendered on the info box
    /// </summary>
    /// <param name="text"></param>
    private void SetupInfoBox(string text)
    {
        this._characterSection.SetActive(false);
        this._infosection.SetActive(true);
        this._infobox.text = text;
    }

    public void AddToQueue(string text)
    {
        Message message = new Message(text);

        _messages.Enqueue(message);
    }

    public void AddToQueue(string text, float time)
    {
        Message message = new Message(text, time);

        _messages.Enqueue(message);
    }

    public void FixedUpdate()
    {
        //if there are no messages in the queue then just keep what is being shown
        if (_messages.Count <= 0)
            return;

        //check how much the text has been showing
        if (_currentMessage != null && _currentTime <= _currentMessage.Duration)
        {
            //if its less then the minimum then add to the timer
            _currentTime += Time.fixedDeltaTime;
        }

        //if its here means that there is something in the queue and has passed enough time in the text
        _currentMessage = _messages.Dequeue();

        SetupInfoBox(_currentMessage.Text);
        
    }

}

public class Message
{
    /// <summary>
    /// The text to display
    /// </summary>
    public string Text { get; set; }
    /// <summary>
    /// How much the text will be showed (at least)
    /// </summary>
    public float Duration { get; set; }
    public Message(string text)
    {
        this.Text = text;
        this.Duration = 1f;
    }

    public Message(string text, float duration)
    {
        this.Text = text;
        this.Duration = duration;
    }
}
