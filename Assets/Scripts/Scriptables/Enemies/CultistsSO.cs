using UnityEngine;

[CreateAssetMenu(fileName = "CultistsSO", menuName = "Scriptable Objects/Enemies/CultistsSO")]
public class CultistsSO : EnemyPartySO
{
    public override EnemyParty CreateParty()
    {
        return new GroupOfCultists(this);
    }
}
