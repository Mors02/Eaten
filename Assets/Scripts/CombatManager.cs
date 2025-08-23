using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public List<CharacterBrain> Party { get; set; }
    [SerializeField]
    private ConnectManager _cn;
    public EnemyManager EnemyParty { get; set; }
    [SerializeField]
    private GameObject _enemyObject;

    [SerializeField]
    private Transform _party;
    //    public GameObject EnemyObject { get; set; }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.Party = GetParty();
        this.EnemyParty = _enemyObject.GetComponent<EnemyManager>();
    }

    public void EnemyTurn()
    {
        this.EnemyParty.Attack();
    }

    public List<CharacterBrain> GetParty()
    {
        List<CharacterBrain> party = new List<CharacterBrain>();
        foreach (Transform transform in _party)
        {
            Debug.Log(transform.gameObject.name);
            party.Add(transform.gameObject.GetComponentInChildren<Connect>().Character);
        }

        return party;
    }
}
