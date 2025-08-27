using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        //foreach (Ability ability in abilities)
        foreach (var ability in abilities.Select((value, i) => new {i, value}))
        {
            Debug.Log(ability.value.Name);
            
            ability.value.Activate(new BattlefieldContext(EnemyParty, Party, GetCharacterInActivatedList(abilities, ability.i-1), GetCharacterInActivatedList(abilities, ability.i+1)));
            foreach (Transform child in partyGraphics)
            {

                if (child.gameObject.name == ability.value.CharacterId.ToString())
                {
                    child.GetComponentInChildren<Animator>().SetTrigger(ability.value.AnimationType.ToString());
                }
            }
            yield return new WaitForSeconds(0.2f);
        }
    }

    public CharacterBrain GetCharacterInActivatedList(List<Ability> list, int index)
    {
        if (index == -1)
            return null;
        if (index == list.Count)
            return null;
        return list[index].Character;
    }

    /// <summary>
    /// Get the whole party of characters
    /// </summary>
    /// <returns>A list of characters that can be used to activate abilities, change stats and retrieve data</returns>
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

    /// <summary>
    /// The character that will activate the ability after the current one (null if current one is the last)
    /// </summary>
    public CharacterBrain NextCharacterInLine { get; set; }
    /// <summary>
    /// The character that activated the ability before the current one (null if current one is the first)
    /// </summary>
    public CharacterBrain LastCharacterInLine { get; set; }

    public BattlefieldContext(EnemyManager enemy, List<CharacterBrain> characters, CharacterBrain next, CharacterBrain last)
    {
        this.EnemyParty = enemy;
        this.Party = characters;
    }

        public BattlefieldContext(EnemyManager enemy, List<CharacterBrain> characters)
    {
        this.EnemyParty = enemy;
        this.Party = characters;
    }

}
