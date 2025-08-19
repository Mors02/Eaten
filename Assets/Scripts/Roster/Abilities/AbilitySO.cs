using UnityEngine;

[CreateAssetMenu(fileName = "AbilitySO", menuName = "Scriptable Objects/AbilitySO")]
public abstract class AbilitySO : ScriptableObject
{
    [SerializeField] protected string abilityName;
    [SerializeField] protected string description;

    public string AbilityName => abilityName;
    public string Description => description;

    public abstract Ability CreateAbility(CharacterBrain character);
}
