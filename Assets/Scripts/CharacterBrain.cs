using UnityEngine;

public abstract class CharacterBrain
{
    public Character _character;
    public string Type
    {
        get => this._character.characterType;
        set => this._character.characterType = value;
    }

    public Sprite Sprite
    {
        get => this._character.sprite;
        set => this._character.sprite = value;
    }

    public int Id { get; set; }

    public string characterName { get; set; }

    public int AbilityId { get; set; }

    public string AbilityPool
    {
        get => this._character.abilityPool;
        set => this._character.abilityPool = value;
    }

    public int Strength
    { get; set; }

    public int Dexterity
    { get; set; }

    public int Intelligence
    { get; set; }

    public int MaxHP
    { get; set; }

    public int CurrentHP
    { get; set; }

    public int Hunger
    { get; set; }

    public string Description
    { 
        get => this._character.description; 
        set => this._character.description = value; 
    }

    public CharacterBrain()
    {
        this.Strength = this._character.baseStrength;
        this.CurrentHP = this.MaxHP = this._character.baseHp;
        this.Hunger = 100;
    }
}
