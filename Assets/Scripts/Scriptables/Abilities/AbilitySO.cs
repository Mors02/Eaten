using UnityEngine;

//[CreateAssetMenu(fileName = "AbilitySO", menuName = "Scriptable Objects/AbilitySO")]
public abstract class AbilitySO : ScriptableObject
{
    [SerializeField] protected string abilityName;
    [SerializeField] protected string description;

    public string AbilityName => abilityName;
    public string Description => description;

    [SerializeField] protected AnimationType _animationType;
    public AnimationType AnimationType => _animationType;

    [SerializeField] protected int _hungerConsumption;

    public int HungerConsumption => _hungerConsumption;

/// <summary>
/// Instantiate the ability class that represents this ability
/// </summary>
/// <param name="character">The character that will use this ability</param>
/// <returns>the ability class instantiated with the correct values</returns>
    public abstract Ability CreateAbility(CharacterData character);
}

public enum AnimationType
{
    Run,
    Jump,
    Row,
    Column,
    Square,
    BigSquare,
    Buff
}
