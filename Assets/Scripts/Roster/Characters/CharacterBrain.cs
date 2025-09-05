using UnityEngine;
using System;
using UnityEngine.Events;
using Unity.VisualScripting;
using System.Collections.Generic;

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

    public static int _maxLevel = 10;

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
    /// The current exp the character has
    /// </summary>
    public int Exp { get; set; }

    /// <summary>
    /// The level the character has reached
    /// </summary>
    public int Level { get; set; }

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

    public CharacterBrain(string id)
    {
        this.Id = Int32.Parse(id);
        this.Hunger = 100;
        _onStatChange = new UnityEvent();
    }

    public CharacterBrain()
    {
        this.Hunger = 100;
        _onStatChange = new UnityEvent();
    }

    public UnityEvent _onStatChange;

    public void ReceiveDamage(int damage)
    {
        this.CurrentHP = Mathf.Max(0, this.CurrentHP -= damage);
        this._onStatChange.Invoke();
    }

    public void Heal(int heal)
    {
        this.CurrentHP = Mathf.Min(this.MaxHP, this.CurrentHP += heal);
        this._onStatChange.Invoke();
    }

    public void ReduceHunger(int hunger)
    {
        this.Hunger -= hunger;
        this._onStatChange.Invoke();
    }

    public void EatEnemy(EnemyParty ep)
    {
        this.Hunger += 20;
        this.LevelUp();
        this._onStatChange.Invoke();
    }

    /// <summary>
    /// Setup all the characters informations
    /// </summary>
    public void SetupCharacter()
    {
        //set up stats
        this.Strength = this._character.baseStrength;
        this.Dexterity = this._character.baseDexterity;
        this.Intelligence = this._character.baseIntelligence;
        this.CurrentHP = this.MaxHP = this._character.baseHp;

        //TODO: this should be random
        this.characterName = CharacterBrain.Names[UnityEngine.Random.Range(0, CharacterBrain.Names.Count)];

        //this.AbilityId = CharacterBrain.GetAbilityFromPool(this.AbilityPool);
        this.Ability = this._character.GetRandomAbility().CreateAbility(new CharacterData(this, this.Id));
    }

    /// <summary>
    /// Pass the id inside the characterbrain to link it to the position inside the grid
    /// </summary>
    /// <param name="id">name of the gameobject square</param>
    public void SetId(int id)
    {
        this.Id = id;
        //this.AbilityId = CharacterBrain.GetAbilityFromPool(this.AbilityPool);
        this.Ability.CharacterId = id;
    }

    public void LevelUp()
    {
        this.Level = Math.Min(this.Level + 1, _maxLevel);
        this.Exp = 0;
    }

    public void GainExp(int exp)
    {
        this.Exp += exp;
        if (this.Exp > NecessaryExp[this.Level])
        {
            this.Exp = this.Exp % NecessaryExp[this.Level];
            this.Level = Math.Min(this.Level + 1, _maxLevel);
        }
    }

    ///https://discussions.unity.com/t/workflow-for-locating-scriptableobjects-at-runtime/681440/2
    //remember this for the randomize of the abilities DEPRACATED
    /*public static int GetAbilityFromPool(string pool)
    {
        string[] abilities = pool.Split(' ');
        int random = UnityEngine.Random.Range(0, abilities.Length - 1);

        return Int32.Parse(abilities[random]);
    }*/

    public static List<string> Names = new List<string>
    {
        "Heinrich",
       "Konrad",
       "Otto",
       "Friedrich",
       "Wilhelm",
       "Gottfried",
       "Albrecht",
       "Bernhard",
       "Dietrich",
       "Engelbert",
       "Gunther",
       "Hartwig",
       "Ludwig",
       "Markward",
       "Norbert",
       "Raimund",
       "Siegfried",
       "Tankred",
       "Ulrich",
       "Walther",
       "Adelheid",
       "Bertha",
       "Cunigunde",
       "Dietlinde",
       "Edeltraud",
       "Gerhild",
       "Hedwig",
       "Irmgard",
       "Kriemhild",
       "Liutgard",
       "Mathilde",
       "Notburga",
       "Richenza",
       "Sieglinde",
       "Thietberga",
       "Uta",
       "Waldburg",
       "Gisela",
       "Brunhild",
       "Ermengarde"
    };

    public static int[] NecessaryExp = {
        0,
        3,
        5,
        9,
        15,
        22,
        30,
        30,
        30,
        30,
        30
    };
}

public class CharacterData
{
    public CharacterBrain characterBrain;
    public int Id;

    public CharacterData(CharacterBrain cb, int id)
    {
        characterBrain = cb;
        Id = id;
    }
}

