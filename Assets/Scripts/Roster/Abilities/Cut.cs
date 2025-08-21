using System.Collections.Generic;
using UnityEngine;

public class Cut : Ability
{

    public override void Activate()
    {
        Debug.Log("Activated CUT: " + this.Description);
    }

    public Cut(CharacterData character, AbilitySO ability, int baseDamage) : base(character, ability)
    {
        this._baseDamage = baseDamage;
        this.Description = Tools.Parametrize(this.BaseDescription, GetSubstitutions());
    }

    public override Dictionary<string, int> GetSubstitutions()
    {
        Dictionary<string, int> subs = new Dictionary<string, int>();
        subs.Add("%damage%", this.BaseDamage + this.Character.Strength);
        return subs;
    }
}
