using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _characterSection, _infosection;

    [SerializeField]
    private TMP_Text _strength, _intelligence, _dexterity, _hunger, _infobox, _name, _abilityDescription, _health;
    // Start is called once before the first execution of Update after the MonoBehaviour is create

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
    public void SetupInfoBox(string text)
    {
        this._characterSection.SetActive(false);
        this._infosection.SetActive(true);
        this._infobox.text = text;
    }

}
