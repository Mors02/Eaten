using System.Linq;
using UnityEngine;

public abstract class EnemyAbility
{

    protected int _baseDamage;
    public int BaseDamage => _baseDamage;
    public string Name { get; set; }

    public AnimationType AnimationType { get; set; }

    public string Description { get; set; }

/// <summary>
/// Activate the ability and the animation
/// </summary>
    public abstract void Activate(BattlefieldContext context);

    protected EnemyParty _party;

    public EnemyParty Party => _party;

    public EnemyAbility(EnemyParty party, EnemyAbilitySO abilityData)
    {
        this._party = party;
        this.Name = abilityData.AbilityName;
        this.Description = abilityData.Description;
        this.AnimationType = abilityData.AnimationType;
    }

    public int CharacterId { get; set; }

    /// <summary>
    /// for attacking abilities, get the corresponding random target
    /// </summary>
    /// <returns>Target, based on the type of the ability, that will receive the attack</returns>
    public Target GetTarget()
    {
        int index;
        Target[] targets;
        switch (this.AnimationType)
        {

            case AnimationType.Row:
                index = Random.Range(0, 3);
                targets = GameObject.FindGameObjectWithTag("Rows").transform.GetComponentsInChildren<Target>();
                foreach (Target target in targets)
                {
                    if (target.gameObject.name == index.ToString())
                        return target;
                }
                return targets[0];
            case AnimationType.Column:
                index = Random.Range(0, 3);
                targets = GameObject.FindGameObjectWithTag("Columns").transform.GetComponentsInChildren<Target>();
                foreach (Target target in targets)
                {
                    if (target.gameObject.name == index.ToString())
                        return target;
                }
                return targets[0];

            default:
                return null;
        }
    }
}
