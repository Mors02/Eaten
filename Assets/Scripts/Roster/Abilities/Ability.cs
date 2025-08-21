using System.Collections.Generic;
using UnityEngine;

public abstract class Ability
{
    protected int _baseDamage;
    public int BaseDamage => _baseDamage;
    public string Name { get; set; }

    public AnimationType AnimationType { get; set; }

    public string BaseDescription { get; set; }

    public string Description { get; set; }

    public abstract void Activate(/*Pass something*/);

    protected CharacterBrain _character;

    public CharacterBrain Character => _character;

    public Ability(CharacterData character, AbilitySO abilityData)
    {
        this._character = character.characterBrain;
        this.CharacterId = character.Id;
        this.Name = abilityData.AbilityName;
        this.BaseDescription = abilityData.Description;
        this.AnimationType = abilityData.AnimationType;
    }

    public int CharacterId { get; set; }

    public abstract Dictionary<string, int> GetSubstitutions();
}
