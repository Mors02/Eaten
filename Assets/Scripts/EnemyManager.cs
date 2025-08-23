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
        Debug.Log("abilities found: " + EnemyParty.Abilities.Count);
        int index = Random.Range(0, EnemyParty.Abilities.Count);
        Debug.Log("Index: " + index);
        this.EnemyParty.Abilities[index].Activate(/*Pass the context*/);
    }
}
