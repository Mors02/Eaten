using UnityEngine;

[CreateAssetMenu(fileName = "ShootSO", menuName = "Scriptable Objects/Abilities/ShootSO")]
public class ShootSO : AbilitySO
{
    [Header("Shoot settings")]
    [SerializeField] private int _baseDamage = 5;
    public int Damage => _baseDamage;
    
    public override Ability CreateAbility(CharacterBrain character)
    {
        return new Shoot(character, this);
    }
}
