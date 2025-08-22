using UnityEngine;

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
