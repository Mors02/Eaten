using UnityEngine;

[CreateAssetMenu(fileName = "PotionSO", menuName = "Scriptable Objects/Items/PotionSO")]
public class PotionSO : ItemSO
{
    public override void Eat(CharacterBrain character)
    {
        character.Heal(Heal);
        character.AddStatus(GameAssets.i.Healing, Heal, Heal);
    }

    public override void Throw(EnemyManager enemy)
    {
        enemy.Heal(Heal);
    }
}
