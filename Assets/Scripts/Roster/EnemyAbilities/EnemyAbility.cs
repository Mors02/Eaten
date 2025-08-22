using UnityEngine;

public abstract class EnemyAbility
{

    protected int _baseDamage;
    public int BaseDamage => _baseDamage;
    public string Name { get; set; }

    public AnimationType AnimationType { get; set; }

    public string Description { get; set; }

    public abstract void Activate(/*Pass something*/);

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
}
