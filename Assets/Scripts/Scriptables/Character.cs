using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Rendering;
[CreateAssetMenu(fileName = "Character", menuName = "Scriptable Objects/Character")]
public class Character : ScriptableObject
{
    public string characterType;
    public int baseHp;
    public string description;
    public int baseStrength;
    public int baseDexterity;
    public int baseIntelligence;
    public Sprite sprite;
    public Sprite shadowSprite;

    [Range(0, 4)]
    public int MaxStatChange = 2;

    [Range(0, 10)]
    public int MaxHealthChange = 5;

    //public string abilityPool;
    [SerializeField] private List<AbilitySO> abilityPool = new List<AbilitySO>();

    public AbilitySO GetRandomAbility()
    {
        if (abilityPool.Count == 0) return null;

        AbilitySO selectedAbility;

        selectedAbility = abilityPool[Random.Range(0, abilityPool.Count)];

        return selectedAbility;
    }
}
