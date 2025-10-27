using UnityEngine;

public class Bites : EnemyAbility
{
    public override void Activate(BattlefieldContext context)
    {
        Target target = this.GetTarget();
        target.DamageTarget(this._baseDamage + Party.Strength);
        target.Animate();
        context.EnemyParty.Animate("EnemyRun");
                
    }

    public Bites(EnemyParty party, BitesSO bitesSO) : base(party, bitesSO)
    {
        this._baseDamage = bitesSO.Damage;
    }
}
