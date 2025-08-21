using System.Collections.Generic;
using UnityEngine;

public class Heal : Ability
{
    public int _baseHeal;
    public int BaseHeal => _baseHeal;
    public override void Activate()
    {
        Debug.Log("Activated HEAL: " + this.Description);
    }

    public override Dictionary<string, int> GetSubstitutions()
    {
        Dictionary<string, int> subs = new Dictionary<string, int>();
        subs.Add("%heal%", this.BaseHeal + this.Character.Intelligence);
        return subs;
    }

    public Heal(CharacterData character, AbilitySO ability, int healValue) : base(character, ability)
    {
        this._baseHeal = healValue;
        this.Description = Tools.Parametrize(this.BaseDescription, GetSubstitutions());

    }
}
