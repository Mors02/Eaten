using UnityEngine;

[CreateAssetMenu(fileName = "ShootSO", menuName = "Scriptable Objects/Abilities/ShootSO")]
public class ShootSO : AbilitySO
{
    [Header("Shoot settings")]
    [SerializeField] private int _baseDamage = 5;

    public override Ability CreateAbility(CharacterBrain character)
    {
        return new Shoot(character, abilityName, description, _baseDamage);
    }
}
