using UnityEngine;

[CreateAssetMenu(fileName = "MeatSO", menuName = "Scriptable Objects/Items/MeatSO")]
public class MeatSO : ItemSO
{
    public override void Eat(CharacterBrain character)
    {
        character.Eat(HungerRestore);
    }

    public override void Throw(EnemyManager enemy)
    {
        enemy.ReceiveDamage(Damage);
    }
}
