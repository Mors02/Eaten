using UnityEngine;

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
    public string abilityPool;

}
