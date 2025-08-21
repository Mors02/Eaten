using UnityEngine;

[CreateAssetMenu(fileName = "HealSO", menuName = "Scriptable Objects/Abilities/HealSO")]
public class HealSO : AbilitySO
{
    [Header("Heal settings")]
    [SerializeField] private int _healAmount = 5;
    public override Ability CreateAbility(CharacterData character)
    {
        return new Heal(character, this, _healAmount);
    }
}
