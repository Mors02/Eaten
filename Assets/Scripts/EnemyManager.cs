using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public EnemyParty EnemyParty { get; set; }
    void Start()
    {
        this.EnemyParty = new GroupOfCultists();
    }

    public void Attack()
    {
        int index = Random.Range(0, EnemyParty.Abilities.Count);
        this.EnemyParty.Abilities[index].Activate(/*Pass the context*/);
    }
}
