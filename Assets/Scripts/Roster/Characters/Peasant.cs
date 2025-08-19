using UnityEngine;

public class Peasant : CharacterBrain
{
    public Peasant() : base()
    {
        this._character = GameAssets.i.Peasant;

        //set up stats
        this.Strength = this._character.baseStrength;
        this.Dexterity = this._character.baseDexterity;
        this.Intelligence = this._character.baseIntelligence;
        this.CurrentHP = this.MaxHP = this._character.baseHp;

        //TODO: this should be random
        this.characterName = "Edgar";

        this.AbilityId = CharacterBrain.GetAbilityFromPool(this.AbilityPool);

    }
}
