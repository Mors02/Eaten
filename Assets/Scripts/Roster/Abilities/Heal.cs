using System.Collections.Generic;
using UnityEngine;

public class Heal : Ability
{
    public int _baseHeal;
    public int BaseHeal => _baseHeal;
    public override void Activate(BattlefieldContext context)
    {
        if (context.NextCharacterInLine != null)
            context.NextCharacterInLine.Heal(_baseHeal + this.Character.Intelligence);
        else
            Debug.Log("Heal from " + this.Character.Type + ": no target");
    }

    public override Dictionary<string, int> GetSubstitutions()
    {
        Dictionary<string, int> subs = new Dictionary<string, int>
        {
            { "%heal%", this.BaseHeal + this.Character.Intelligence }
        };
        return subs;
    }

    public Heal(CharacterData character, HealSO ability) : base(character, ability)
    {
        this._baseHeal = ability.Heal;
        this.Description = Tools.Parametrize(this.BaseDescription, GetSubstitutions());

    }
}
