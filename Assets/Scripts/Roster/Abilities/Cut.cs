using System.Collections.Generic;
using UnityEngine;

public class Cut : Ability
{

    public override void Activate(BattlefieldContext context)
    {
        context.EnemyParty.ReceiveDamage(this.BaseDamage + this.Character.Strength);
    }

    public Cut(CharacterData character, CutSO ability) : base(character, ability)
    {
        this._baseDamage = ability.Damage;
        this.Description = Tools.Parametrize(this.BaseDescription, GetSubstitutions());
    }

    public override Dictionary<string, int> GetSubstitutions()
    {
        Dictionary<string, int> subs = new Dictionary<string, int>
        {
            { "%damage%", this.BaseDamage + this.Character.Strength }
        };
        return subs;
    }
}
