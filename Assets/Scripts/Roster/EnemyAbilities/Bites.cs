using UnityEngine;

public class Bites : EnemyAbility
{
    public override void Activate()
    {
        Debug.Log("Activated BITES: ");
    }

    public Bites(EnemyParty party, BitesSO bitesSO) : base(party, bitesSO)
    {
        this._baseDamage = bitesSO.Damage;
    }
}
