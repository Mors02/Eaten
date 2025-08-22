using UnityEngine;

[CreateAssetMenu(fileName = "AbilitySO", menuName = "Scriptable Objects/AbilitySO")]
public abstract class AbilitySO : ScriptableObject
{
    [SerializeField] protected string abilityName;
    [SerializeField] protected string description;

    public string AbilityName => abilityName;
    public string Description => description;

    [SerializeField] protected AnimationType _animationType;
    public AnimationType AnimationType => _animationType;

    public abstract Ability CreateAbility(CharacterData character);
}

public enum AnimationType
{
    Run,
    Jump,
    Line,
    Square,
    BigSquare
}
