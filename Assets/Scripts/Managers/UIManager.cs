using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _characterSection, _infosection;

    [SerializeField]
    private TMP_Text _strength, _intelligence, _dexterity, _hunger, _infobox, _name, _abilityDescription, _health;
    // Start is called once before the first execution of Update after the MonoBehaviour is create

    public void SetupCharacter(CharacterBrain cb)
    {
        this._characterSection.SetActive(true);
        this._infosection.SetActive(false);
        this._strength.text = cb.Strength.ToString();
        this._dexterity.text = cb.Dexterity.ToString();
        this._intelligence.text = cb.Intelligence.ToString();
        this._hunger.text = cb.Hunger.ToString();
        this._abilityDescription.text = cb.Ability.Description;
        this._name.text = cb.characterName;
        this._health.text = cb.CurrentHP + "/" + cb.MaxHP;
    }

    public void SetupInfoBox(string text)
    {
        this._characterSection.SetActive(false);
        this._infosection.SetActive(true);
        this._infobox.text = text;
    }
}
