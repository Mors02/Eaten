using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        this.EnemyParty.Attack(new BattlefieldContext(EnemyParty, Party));
    }

    public void PlayerTurn(List<Ability> abilities, Transform partyGraphics)
    {

        IEnumerator coroutine = AnimateCharacters(abilities, partyGraphics);
        StartCoroutine(coroutine);
        Invoke("EnemyTurn", 2f);
    }

    public IEnumerator AnimateCharacters(List<Ability> abilities, Transform partyGraphics) {
        Debug.Log("There are " + abilities.Count + " abilities");
        foreach (Ability ability in abilities)
        {
            Debug.Log(ability.Name);
            ability.Activate(new BattlefieldContext(EnemyParty, Party));
            foreach (Transform child in partyGraphics)
            {

                if (child.gameObject.name == ability.CharacterId.ToString())
                {
                    child.GetComponentInChildren<Animator>().SetTrigger(ability.AnimationType.ToString());
                }
            }
            yield return new WaitForSeconds(0.2f);
        }
    }

    public List<CharacterBrain> GetParty()
    {
        List<CharacterBrain> party = new List<CharacterBrain>();
        foreach (Transform transform in _party)
        {
            party.Add(transform.gameObject.GetComponentInChildren<Connect>().Character);
        }

        return party;
    }
}

/// <summary>
/// Class used to pass the battlefield informations to the activated ability
/// </summary>
public class BattlefieldContext
{
    /// <summary>
    /// Enemy party with all the informations (both logic and graphics)
    /// </summary>
    public EnemyManager EnemyParty { get; set; }

    /// <summary>
    /// List of all the characters and their status (only logic)
    /// </summary>
    public List<CharacterBrain> Party { get; set; }

    public BattlefieldContext(EnemyManager enemy, List<CharacterBrain> characters)
    {
        this.EnemyParty = enemy;
        this.Party = characters;
    }

}
