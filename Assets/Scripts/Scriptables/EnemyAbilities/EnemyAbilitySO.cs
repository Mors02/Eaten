using UnityEngine;

public abstract class EnemyAbilitySO : ScriptableObject
{
    [SerializeField] protected string abilityName;
    [SerializeField] protected string description;

    public string AbilityName => abilityName;
    public string Description => description;

    [SerializeField] protected AnimationType _animationType;
    public AnimationType AnimationType => _animationType;

    public abstract EnemyAbility CreateAbility(EnemyParty party);
}
