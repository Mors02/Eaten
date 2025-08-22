using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "EnemyPartySO", menuName = "Scriptable Objects/EnemyPartySO")]
public class EnemyPartySO : ScriptableObject
{
    [SerializeField]
    private string _name;

    public string Name => _name;

    [SerializeField]
    private List<Character> _characters;

    [SerializeField]
    private List<EnemyAbilitySO> _abilities;

    public List<EnemyAbilitySO> Abilities => _abilities;

    public List<Character> Characters => _characters;
    public int baseHp;
    public int baseStrength;
    public int baseDexterity;
    public int baseIntelligence;
}
