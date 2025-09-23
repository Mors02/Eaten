using UnityEngine;

[CreateAssetMenu(fileName = "HealSO", menuName = "Scriptable Objects/Abilities/HealSO")]
public class HealSO : AbilitySO
{
    [Header("Heal settings")]
    [SerializeField] private int _healAmount = 5;

    public int Heal => _healAmount;
    public override Ability CreateAbility(CharacterBrain character)
    {
        return new Heal(character, this);
    }
}
