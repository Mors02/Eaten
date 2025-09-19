using UnityEngine;

[CreateAssetMenu(fileName = "BonesSO", menuName = "Scriptable Objects/Items/BonesSO")]
public class BonesSO : ItemSO
{
    public override void Eat(CharacterBrain character)
    {
        character.Eat(HungerRestore);
        character.Heal(Heal);
    }

    public override void Throw(EnemyManager enemy)
    {
        enemy.ReceiveDamage(Damage);
    }
}
