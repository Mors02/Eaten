using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public EnemyParty EnemyParty { get; set; }

    public List<Transform> EnemyPositions { get; set; }

    [Range(0, 100)]
    [SerializeField]
    private int _minimumHealthToEat;

    /// <summary>
    /// All the animators of the characters in the enemy party
    /// </summary>
    public List<Animator> Animators { get; set; }

    [SerializeField]
    private GameObject _characterPrefab;

    [SerializeField]
    private Vector2 _area;

    void Start()
    {
        this.EnemyParty = GameManager.i.SelectedEvent.enemyParty.CreateParty();
        this.Animators = new List<Animator>();
        this.EnemyPositions = new List<Transform>();
        SetupCharacterPrefabs();
        foreach (Transform child in this.transform)
        {
            this.Animators.Add(child.GetComponent<Animator>());
        }
    }

    public void ReceiveDamage(int damage)
    {
        this.EnemyParty.CurrentHP -= damage;
        if (((float)EnemyParty.CurrentHP / EnemyParty.MaxHP) * 100 <= _minimumHealthToEat)
        {

            GameManager.i.CanEat = true;
        }
        this.EnemyParty._onStatsChange.Invoke();
        
    }

    public void Heal(int heal)
    {
        this.EnemyParty.CurrentHP += heal;
        
        if ((float)EnemyParty.CurrentHP / EnemyParty.MaxHP * 100 > _minimumHealthToEat)
            GameManager.i.CanEat = false;

        this.EnemyParty._onStatsChange.Invoke();


    }

    public void Attack(BattlefieldContext context)
    {        
        int index = Random.Range(0, EnemyParty.Abilities.Count);
        this.EnemyParty.Abilities[index].Activate(context);
        GameAssets.i.UiManager.SetupInfoBox(this.EnemyParty.Abilities[index].Description);
    }

    public void Animate(string animation)
    {
        IEnumerator coroutine = AnimateCharacters(animation);
        StartCoroutine(coroutine);
    }

    private IEnumerator AnimateCharacters(string animation)
    {
        foreach (Animator animator in Animators)
        {
            if (animator != null)
            {
                animator.SetTrigger(animation);
                yield return new WaitForSeconds(0.2f);  
            }
                
        }

    }

    void OnDrawGizmosSelected()
    {

#if UNITY_EDITOR
        Gizmos.color = Color.red;
        Gizmos.DrawCube(new Vector3(this.transform.position.x, this.transform.position.y, 0), _area);

        Gizmos.color = Color.white;
#endif
    }

    void SetupCharacterPrefabs()
    {
        List<SpriteRenderer> prefabs = new List<SpriteRenderer>();
        foreach (Character character in EnemyParty.Characters)
        {
            Vector3 randomPos = new Vector3(Random.Range(-(_area.x / 2), _area.x / 2), Random.Range(-(_area.y / 2), _area.y / 2), 0);
            GameObject prefab = Instantiate(_characterPrefab, this.transform.position + randomPos, Quaternion.identity, this.transform);
            prefab.transform.localScale = new Vector3(-1, 1, 1);
            SpriteRenderer s = prefab.GetComponentInChildren<SpriteRenderer>();
            s.sprite = character.sprite;
            prefabs.Add(s);
            EnemyPositions.Add(prefab.transform);
        }
        prefabs.Sort(OrderBasedOnY);
        for (int i = 0; i < prefabs.Count; i++)
        {
            prefabs[i].sortingOrder = i;
        }

    }

    private static int OrderBasedOnY(SpriteRenderer g1, SpriteRenderer g2)
    {
        if (g1.transform.position.y < g2.transform.position.y)
            return 1;
        return -1;
    }
}
