using UnityEngine;

[CreateAssetMenu(fileName = "BitesSO", menuName = "Scriptable Objects/EnemyAbilities/BitesSO")]
public class BitesSO : EnemyAbilitySO
{
    [Header("Bites informations")]
    [SerializeField] private int _damage;
    public int Damage => _damage;
    public override EnemyAbility CreateAbility(EnemyParty party)
    {
        return new Bites(party, this);
    }

}
