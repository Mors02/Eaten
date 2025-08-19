using UnityEngine;

[CreateAssetMenu(fileName = "CutSO", menuName = "Scriptable Objects/CutSO")]
public class CutSO : AbilitySO
{
    [Header("Cut settings")]
    [SerializeField] private int baseDamage = 5;

    public override Ability CreateAbility(CharacterBrain character)
    {
        return new Cut(character, abilityName, description, baseDamage);
    }
}
