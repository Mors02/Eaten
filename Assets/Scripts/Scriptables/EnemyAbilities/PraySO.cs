using UnityEngine;

[CreateAssetMenu(fileName = "PraySO", menuName = "Scriptable Objects/EnemyAbilities/PraySO")]
public class PraySO : EnemyAbilitySO
{
    [Header("Bites informations")]
    [SerializeField] private int _heal;
    public int Heal => _heal;
    public override EnemyAbility CreateAbility(EnemyParty party)
    {
        return new Pray(party, this);
    }

}
