using UnityEngine;

public class GroupOfCultists : EnemyParty
{
    public GroupOfCultists()
    {
        this._enemyParty = GameAssets.i.cultists;
        this.Strength = this._enemyParty.baseStrength;
        this.Dexterity = this._enemyParty.baseDexterity;
        this.Intelligence = this._enemyParty.baseIntelligence;
        this.CurrentHP = this.MaxHP = this._enemyParty.baseHp;

        this.Abilities = this.GetAbilities();
    }
}
