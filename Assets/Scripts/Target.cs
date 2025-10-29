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

    public CombatManager Party { get; set; }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.Index = Int32.Parse(this.gameObject.name);
        this.Characters = new List<CharacterBrain>();
        this.Party = GameObject.FindGameObjectWithTag("Canvas").GetComponent<CombatManager>();
        this.PopulateTarget();
        this._animator = GetComponent<Animator>();
    }

    /// <summary>
    /// Populate the current target with all the characters that will be hit by the enemy.
    /// </summary>
    public void PopulateTarget()
    {
        List<CharacterBrain> characters = Party.GetParty();
        switch (this.TargetType)
        {
            case AnimationType.Row:
                for (int i = Index * 3; i < (Index * 3) + 3; i++)
                {
                    //Debug.Log(Index + ": Looking for " + i + "; " + (characters.ElementAt(i) == null? "Not found" : "Found"));
                    //what happens if the row is not full? idk
                    CharacterBrain ch = i >= characters.Count? null : characters.ElementAt(i);
                    if (ch != null)
                        this.Characters.Add(ch);
                }
                break;

            case AnimationType.Column:
                for (int i = Index; i < Index + 6; i = i + 3)
                {
                    // Debug.Log("Looking for " + i + "; " + (characters.ElementAt(i) == null? "Not found" : "Found"));
                    //what happens if the column is not full? idk
                    CharacterBrain ch = i >= characters.Count? null : characters.ElementAt(i);
                    if (ch != null)
                        this.Characters.Add(ch);
                }
                break;
        }
    }

    public void DamageTarget(int damage)
    {
        //Debug.Log("Row " + Index + ", " + Characters.Count + " characters");
        foreach (CharacterBrain ch in Characters)
        {
            //animation params
            Party.Graphics[ch.Id].SetDamage(damage);
            Party.Graphics[ch.Id].PlayAnimation("Hit");
            ch.ReceiveDamage(damage);
        }
    }

    public void Animate()
    {
        this._animator.SetTrigger(this._targetType.ToString());
    }
}
