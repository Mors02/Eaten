using UnityEngine;

public class HolyGroup : EnemyParty
{
    public HolyGroup(EnemyPartySO party)
    {
        this._enemyParty = party;
        this.Strength = this._enemyParty.baseStrength;
        this.Dexterity = this._enemyParty.baseDexterity;
        this.Intelligence = this._enemyParty.baseIntelligence;
        this.CurrentHP = this.MaxHP = this._enemyParty.baseHp;

        this.Abilities = this.GetAbilities();
    }
}
