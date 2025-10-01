using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;
using Unity.VisualScripting;

public class EnemyParty
{

    public EnemyPartySO _enemyParty;
    public List<Character> _characters;

    public string Name
    {
        get => this._enemyParty.Name;
        //set => this._enemyParty.Name = value;
    }

    public string Description
    {
        get => this._enemyParty.Description;
    }

    public List<Character> Characters { get => this._enemyParty.Characters; }

    public List<EnemyAbility> Abilities { get; set; }

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

    public Dictionary<StatusName, Status> _statuses;

    public EnemyParty()
    {
        this._onStatsChange = new UnityEvent();
        this._onStatusChange = new UnityEvent();
        this._statuses = new Dictionary<StatusName, Status>();
    }

    public UnityEvent _onStatsChange;
    public UnityEvent _onStatusChange;

    public List<EnemyAbility> GetAbilities()
    {
        List<EnemyAbility> list = new List<EnemyAbility>();
        foreach (EnemyAbilitySO ability in _enemyParty.Abilities)
        {
            list.Add(ability.CreateAbility(this));
        }

        return list;
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
}
