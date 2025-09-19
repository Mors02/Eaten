using UnityEngine;

[CreateAssetMenu(fileName = "RockSO", menuName = "Scriptable Objects/Items/RockSO")]
public class RockSO : ItemSO
{
    public override void Eat(CharacterBrain character)
    {
        character.Eat(HungerRestore);
        character.ReceiveDamage(HungerRestore);
    }

    public override void Throw(EnemyManager enemy)
    {
        enemy.ReceiveDamage(Damage);
    }
}
