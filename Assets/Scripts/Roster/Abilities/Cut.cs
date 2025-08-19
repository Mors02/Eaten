using System.Collections.Generic;
using UnityEngine;

public class Cut : Ability
{
    protected int _baseDamage;
    public int BaseDamage => _baseDamage;

    public override void Activate()
    {
        
    }

    public Cut(CharacterBrain character, string name, string description, int baseDamage) : base(name, description)
    {
        this._character = character;
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
