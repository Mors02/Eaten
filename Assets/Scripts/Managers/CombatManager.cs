using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class CombatManager : MonoBehaviour
{
    public List<CharacterBrain> Party { get; set; }
    [SerializeField]
    private ConnectManager _cn;
    public EnemyManager EnemyParty { get; set; }
    [SerializeField]
    private GameObject _enemyObject;

    /// <summary>
    /// Party in the UI
    /// </summary>
    [SerializeField]
    private Transform _party;

    /// <summary>
    /// Party graphics in the gameworld
    /// </summary>
    [SerializeField]
    private Transform _partyGraphics;

    [Range(0, 100)]
    [SerializeField]
    private int _dropRate;

    [SerializeField]
    private Transform _dropParent;

    private int _howManyDrops;

    private CharacterGraphics[] _graphics;
    public CharacterGraphics[] Graphics => _graphics;
    //    public GameObject EnemyObject { get; set; }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameManager.i.CanPlay = true;
        GameManager.i.EnemiesEaten = 0;
        GameManager.i.CharactersDead = 0;
        _howManyDrops = 0;
        this.Party = GetParty();
        this.EnemyParty = _enemyObject.GetComponent<EnemyManager>();
        this._graphics = new CharacterGraphics[_partyGraphics.childCount];
        foreach (Transform child in _partyGraphics)
        {
            _graphics[Int32.Parse(child.name)] = child.GetComponentInChildren<CharacterGraphics>();
        }
    }
    /// <summary>
    /// decides what the enemies will do
    /// </summary>
    public void EnemyTurn(float delay = 2f)
    {
        GameManager.i.CanPlay = false;
        Invoke("EnemyAction", delay);
    }

    /// <summary>
    /// Called when the enemy party cant continue fighting
    /// </summary>
    /// <param name="run">True if there are survivors that should run away as an animation</param>
    public void EnemyOutOfAction(bool run)
    {
        GameManager.i.CanPlay = false;
        for (int i = 0; i < Party.Count; i++)
        {
            _graphics[i].PlayAnimation("Jump");
        }


    }

    public void BackToBar()
    {
        SceneManager.LoadScene("PartyScene");
    }

    private void EnemyAction()
    {

        if (this.EnemyParty.EnemyPositions.Count == 0)
        {
            SetupDrop();
            EndCombatChecks();
            EnemyOutOfAction(false);
            GameAssets.i.UiManager.AddToQueue("The enemies are escaping!");
            return;
        }
        this.EnemyParty.Attack(new BattlefieldContext(EnemyParty, Party));
        Invoke("CanPlay", 2f);
    }

    /// <summary>
    /// activate what the player decided during their turn
    /// </summary>
    /// <param name="abilities">the list of abilities in the order of activation</param>
    /// <param name="partyGraphics">the transform that contains the party graphics</param>
    public void PlayerTurn(List<Ability> abilities)
    {
        IEnumerator coroutine = AnimateCharacters(abilities);
        StartCoroutine(coroutine);
        EnemyTurn();
    }

    private void CanPlay()
    {
        GameManager.i.CanPlay = true;
        foreach (CharacterBrain character in Party)
        {
            character.ActivateStatuses();
        }
        EnemyParty.ActivateStatuses();
    }

    public IEnumerator AnimateCharacters(List<Ability> abilities)
    {
        //foreach (Ability ability in abilities)
        foreach (var ability in abilities.Select((value, i) => new { i, value }))
        {
            BattlefieldContext context = new BattlefieldContext(EnemyParty, Party, GetCharacterInActivatedList(abilities, ability.i + 1), GetCharacterInActivatedList(abilities, ability.i - 1));
            ability.value.Activate(context);
            _graphics[ability.value.CharacterId].PlayAnimation(ability.value.AnimationType.ToString());

            yield return new WaitForSeconds(0.2f);
        }
    }

    public void EatAnimation(int Id, Item item)
    {
        _graphics[Id].EatAnimation(item);
    }

    /// <summary>
    /// A bit clunky but removes all the highlights from the party. Cycles the gameobject and removes the border
    /// </summary>
    public void RemoveAllHighlights()
    {
        foreach (Transform child in this._party)
        {
            child.gameObject.GetComponent<Connect>().Highlight(false);
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
            Connect connect = transform.gameObject.GetComponentInChildren<Connect>();
            if (connect.Character != null) {
                party.Add(connect.Character);
            }
            
        }

        return party;
    }

    public void EndCombatChecks()
    {
        foreach (CharacterBrain brain in Party)
        {
            brain.EndCombatChecks();
        }
    }
    public void SetupDrop()
    {
        foreach (Transform child in _dropParent)
        {
            ItemUI ui = child.gameObject.GetComponent<ItemUI>();
            //see if an item is dropped
            if (UnityEngine.Random.Range(0, 100) < _dropRate)
            {
                //generate the new item
                ui.SetItem(new Item(GameAssets.i.items[UnityEngine.Random.Range(0, GameAssets.i.items.Count)]));
                _howManyDrops++;
            }
            else
            {
                ui.Inactive();
            }
            //halve the drop rate for the followings items;
            //_dropRate = _dropRate / 2;
        }

        GameAssets.i.UiManager.SetupEndscreen(_howManyDrops);
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
        this.NextCharacterInLine = next;
        this.LastCharacterInLine = last;
    }

        public BattlefieldContext(EnemyManager enemy, List<CharacterBrain> characters)
    {
        this.EnemyParty = enemy;
        this.Party = characters;
    }

}
