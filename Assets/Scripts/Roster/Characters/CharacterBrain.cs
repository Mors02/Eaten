using UnityEngine;
using System;

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

    /// <summary>
    /// Random value
    /// </summary>
    public string characterName { get; set; }

    /// <summary>
    /// Random value
    /// </summary>
    public int AbilityId { get; set; }    

    public Ability Ability { get; set; }

    /*public string AbilityPool
    {
        get => this._character.abilityPool;
        set => this._character.abilityPool = value;
    }*/

    /// <summary>
    /// based on the base strength
    /// </summary>
    public int Strength
    { get; set; }

    /// <summary>
    /// based on the base dexterity
    /// </summary>
    public int Dexterity
    { get; set; }

    /// <summary>
    /// based on the base intelligence
    /// </summary>
    public int Intelligence
    { get; set; }

    /// <summary>
    /// Random value
    /// </summary>
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
        this.Hunger = 100;
    }


    ///https://discussions.unity.com/t/workflow-for-locating-scriptableobjects-at-runtime/681440/2
    //remember this for the randomize of the abilities DEPRACATED
    /*public static int GetAbilityFromPool(string pool)
    {
        string[] abilities = pool.Split(' ');
        int random = UnityEngine.Random.Range(0, abilities.Length - 1);

        return Int32.Parse(abilities[random]);
    }*/
}
