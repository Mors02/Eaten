using System.Collections.Generic;
using UnityEngine;

public class Shoot : Ability
{
    public override void Activate(BattlefieldContext context)
    {
        Debug.Log("Activated SHOOT: " + this.Description);
    }

    public override Dictionary<string, int> GetSubstitutions()
    {
        Dictionary<string, int> subs = new Dictionary<string, int>
        {
            { "%damage%", this.BaseDamage + this.Character.Dexterity }
        };
        return subs;
    }

    public Shoot(CharacterData character, ShootSO abilityData) : base(character, abilityData)
    {
        this._baseDamage = abilityData.Damage;
        this.Description = Tools.Parametrize(BaseDescription, GetSubstitutions());
    }
}
