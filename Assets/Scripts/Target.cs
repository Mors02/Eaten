using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Target : MonoBehaviour
{
    public int Index { get; set; }
    [SerializeField]
    private AnimationType _targetType;
    public AnimationType TargetType { get => _targetType; set => _targetType = value; }

    private Animator _animator;

    public List<CharacterBrain> Characters { get; set; }

    public CombatManager party { get; set; }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.Index = Int32.Parse(this.gameObject.name);
        this.Characters = new List<CharacterBrain>();
        this.party = GameObject.FindGameObjectWithTag("Canvas").GetComponent<CombatManager>();
        this.PopulateTarget();
        this._animator = GetComponent<Animator>();
    }

    /// <summary>
    /// Populate the current target with all the characters that will be hit by the enemy.
    /// </summary>
    public void PopulateTarget()
    {
        List<CharacterBrain> characters = party.GetParty();
        switch (this.TargetType)
        {
            case AnimationType.Row:
                for (int i = Index * 3; i < (Index * 3) + 3; i++)
                {
                    //Debug.Log(Index + ": Looking for " + i + "; " + (characters.ElementAt(i) == null? "Not found" : "Found"));
                    //what happens if the row is not full? idk
                    this.Characters.Add(characters.ElementAt(i));
                }
                break;

            case AnimationType.Column:
                for (int i = Index; i < Index + 6; i = i + 3)
                {
                    // Debug.Log("Looking for " + i + "; " + (characters.ElementAt(i) == null? "Not found" : "Found"));
                    //what happens if the column is not full? idk
                    this.Characters.Add(characters.ElementAt(i));
                }
                break;
        }
    }

    public void DamageTarget()
    {
        //Debug.Log("Row " + Index + ", " + Characters.Count + " characters");
        foreach (CharacterBrain ch in Characters)
        {
            Debug.Log(ch.Id + " received damage");
        }
    }

    public void Animate()
    {
        this._animator.SetTrigger(this._targetType.ToString());
    }
}
