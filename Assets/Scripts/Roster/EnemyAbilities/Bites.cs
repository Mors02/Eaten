using UnityEngine;

public class Bites : EnemyAbility
{
    public override void Activate()
    {
        Target target = this.GetTarget();
        Debug.Log("Activated BITES: " + target.gameObject.name);
        target.DamageTarget();
                
    }

    public Bites(EnemyParty party, BitesSO bitesSO) : base(party, bitesSO)
    {
        this._baseDamage = bitesSO.Damage;
    }
}
