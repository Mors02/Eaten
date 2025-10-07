using UnityEngine;
using System;
using UnityEngine.Events;
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

    public bool HasLeveledUp { get; set; }

    private int _injuryProb = 0;

    private Dictionary<StatusName, Status> _statuses;

    /// <summary>
    /// represents how much human meat the character has eaten
    /// </summary>
    public int Bloodthirst { get; set; }

    public string Description
    {
        get => this._character.description;
        set => this._character.description = value;
    }

    public CharacterBrain(string id)
    {
        this.Id = Int32.Parse(id);
        this.Hunger = 100;
        this.Level = 1;
        this.Exp = 0;
        this.Bloodthirst = 0;
        this._injuryProb = 0;
        _onStatChange = new UnityEvent();
        _onStatusChange = new UnityEvent();
        _statuses = new Dictionary<StatusName, Status>();
    }

    public CharacterBrain()
    {
        this.Hunger = 100;
        this.Level = 1;
        this.Exp = 0;
        this.Bloodthirst = 0;
        this._injuryProb = 0;
        _onStatChange = new UnityEvent();
        _onStatusChange = new UnityEvent();
        _statuses = new Dictionary<StatusName, Status>();
    }

    public UnityEvent _onStatChange;
    public UnityEvent _onStatusChange;

    /// <summary>
    /// Damage the character
    /// </summary>
    /// <param name="damage">Damage amount</param>
    public void ReceiveDamage(int damage)
    {
        Debug.Log(this.CurrentHP);
        if (this.CurrentHP <= 0)
        {
            RollForInjury();
        }
        this.CurrentHP = Mathf.Max(0, this.CurrentHP -= damage);
        this._onStatChange.Invoke();
    }

    /// <summary>
    /// Heal the character
    /// </summary>
    /// <param name="heal">Healing amount</param>
    public void Heal(int heal)
    {
        this.CurrentHP = Mathf.Min(this.MaxHP, this.CurrentHP += heal);
        this._onStatChange.Invoke();
    }

    /// <summary>
    /// Reduce the total hunger
    /// </summary>
    /// <param name="hunger">How much hunger to replenish</param>
    public void ReduceHunger(int hunger)
    {
        this.Hunger -= hunger;
        this._onStatChange.Invoke();
    }

    /// <summary>
    /// Eat the enemy
    /// </summary>
    /// <param name="ep">Enemy eaten</param>
    public void EatEnemy(EnemyParty ep)
    {
        this.Eat(20);
        this.LevelUp();
        this._onStatChange.Invoke();
    }

    public void AddStatus(StatusSO info, int duration, int value, string text="")
    {
        this._statuses.Add(info.Name, new Status(info, duration, value, text));
        this._onStatusChange.Invoke();
    }

    public void RemoveStatus(StatusName name)
    {
        this._statuses.Remove(name);
        this._onStatusChange.Invoke();
    }

    public void RollForInjury()
    {
        Debug.Log("ROlling injury");
        //Roll percentage
        int prob = UnityEngine.Random.Range(0, 100);
        if (prob <= _injuryProb)
        {
            //Reduce stats
            switch (UnityEngine.Random.Range(0, 4))
            {
                case 0:
                    this.Strength--;
                    AddStatus(GameAssets.i.Injured, -1, this.Strength, "strength");
                    break;
                case 1:
                    this.Dexterity--;
                    AddStatus(GameAssets.i.Injured, -1, this.Dexterity, "dexterity");
                    break;
                case 2:
                    this.Intelligence--;
                    AddStatus(GameAssets.i.Injured, -1, this.Intelligence, "intelligence");
                    break;
                case 3:
                    this.MaxHP -= 3;
                    AddStatus(GameAssets.i.Injured, -1, this.CurrentHP, "health");
                    break;
            }
        }
        else
        {
            //Else increase injuryProbability
            this._injuryProb += 60;
        }
            
    }

    public void Eat(int hunger)
    {
        this.Hunger += hunger;
        this.Bloodthirst++;
    }

    public void EndCombatChecks()
    {
        if (HasLeveledUp)
        {
            this.AddStatus(GameAssets.i.LevelUp, -1, this.Level);
            this.HasLeveledUp = false;
        }
            
    }

    /// <summary>
    /// Setup all the characters informations
    /// </summary>
    public void SetupCharacter()
    {
        //set up stats
        /* this.Strength = this._character.baseStrength;
         this.Dexterity = this._character.baseDexterity;
         this.Intelligence = this._character.baseIntelligence;
         this.CurrentHP = this.MaxHP = this._character.baseHp;*/
        RandomizeStats();

        //TODO: this should be random
        this.characterName = Names[UnityEngine.Random.Range(0, Names.Count)];

        //this.AbilityId = CharacterBrain.GetAbilityFromPool(this.AbilityPool);
        this.Ability = this._character.GetRandomAbility().CreateAbility(this);
    }

    public void RandomizeStats()
    {
        this.Strength = this._character.baseStrength + (UnityEngine.Random.Range(0, _character.MaxStatChange) - _character.MaxStatChange/2);
        this.Dexterity = this._character.baseDexterity + (UnityEngine.Random.Range(0, _character.MaxStatChange) - _character.MaxStatChange/2);
        this.Intelligence = this._character.baseIntelligence + (UnityEngine.Random.Range(0, _character.MaxStatChange) - _character.MaxStatChange/2);
        this.CurrentHP = this.MaxHP = this._character.baseHp + (UnityEngine.Random.Range(0, _character.MaxHealthChange) - _character.MaxHealthChange/2);
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
        this.HasLeveledUp = true;
    }

    public void ActivateStatuses()
    {
        Status status;
        if (Has(StatusName.Healing, out status))
        {
            this.Heal(status.Value);
        }

        if (Has(StatusName.Bleeding, out status))
        {
            this.ReceiveDamage(status.Value);
        }

        TickDownStatuses();
    }

    public bool Has(StatusName name, out Status status) {
        try
        {
            status = _statuses[name];
            return _statuses[name] != null;
        }
        catch (Exception e)
        {
            status = null;
            return false;
        }
        
    }

    public void TickDownStatuses()
    {
        List<StatusName> statusesToRemove = new List<StatusName>();
        foreach (Status status in _statuses.Values)
        {
            status.TickDown();
            if (status.Duration == 0)
                statusesToRemove.Add(status.Info.Name);
        }

        foreach (StatusName name in statusesToRemove)
        {
            RemoveStatus(name);
        }
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

    public List<Status> GetStatuses()
    {
        List<Status> statuses = new List<Status>();
        foreach (Status status in _statuses.Values)
        {
            statuses.Add(status);
        }

        return statuses;
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

