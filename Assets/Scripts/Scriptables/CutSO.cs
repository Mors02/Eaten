using UnityEngine;

[CreateAssetMenu(fileName = "CutSO", menuName = "Scriptable Objects/Abilities/CutSO")]
public class CutSO : AbilitySO
{
    [Header("Cut settings")]
    [SerializeField] private int _baseDamage = 5;

    public override Ability CreateAbility(CharacterData character)
    {
        return new Cut(character, this, _baseDamage);
    }
}
