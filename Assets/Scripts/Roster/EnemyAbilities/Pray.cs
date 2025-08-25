using System.Net.Mime;
using UnityEngine;

public class Pray : EnemyAbility
{
    private int _heal;
    public override void Activate(BattlefieldContext context)
    {
        Debug.Log("Activated PRAY: ");
        context.EnemyParty.Animate("Jump");
    }

    public Pray(EnemyParty party, PraySO ability) : base(party, ability)
    {
        this._heal = ability.Heal;
    }
}
