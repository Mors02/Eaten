using UnityEngine;

[CreateAssetMenu(fileName = "HolyGroupSO", menuName = "Scriptable Objects/Enemies/HolyGroupSO")]
public class HolyGroupSO : EnemyPartySO
{
    public override EnemyParty CreateParty()
    {
        return new HolyGroup(this);
    }
}
