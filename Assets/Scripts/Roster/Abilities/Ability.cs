using System.Collections.Generic;
using UnityEngine;

public abstract class Ability
{
    protected int _baseDamage;
    public int BaseDamage => _baseDamage;
    public string Name { get; set; }

    public int HungerConsumption { get; set; }

    public AnimationType AnimationType { get; set; }

    public string BaseDescription { get; set; }

    public string Description { get; set; }

/// <summary>
/// Activate the ability
/// </summary>
    public virtual void Activate(BattlefieldContext context)
    {
        this._character.ReduceHunger(this.HungerConsumption);
    }

    protected CharacterBrain _character;

    public CharacterBrain Character => _character;

    public Ability(CharacterBrain character, AbilitySO abilityData)
    {
        this._character = character;
        this.CharacterId = character.Id;
        this.Name = abilityData.AbilityName;
        this.BaseDescription = abilityData.Description;
        this.AnimationType = abilityData.AnimationType;
        this.HungerConsumption = abilityData.HungerConsumption;
    }

    public int CharacterId { get; set; }

/// <summary>
/// get the dictionary that contains the substitutions inside the description to make it dynamic with the value changes (based on the stats of the character)
/// </summary>
/// <returns>Dictionary<string, int> that contains the string to substitute and the corresponding value</returns>
    public abstract Dictionary<string, int> GetSubstitutions();
}
