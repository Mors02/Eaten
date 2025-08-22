using UnityEngine;

public class Pray : EnemyAbility
{
    private int _heal;
    public override void Activate()
    {
        Debug.Log("Activated PRAY: ");
    }

    public Pray(EnemyParty party, PraySO ability) : base(party, ability)
    {
        this._heal = ability.Heal;
    }
}
