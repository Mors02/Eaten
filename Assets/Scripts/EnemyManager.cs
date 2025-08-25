using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public EnemyParty EnemyParty { get; set; }

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
        this.EnemyParty = new GroupOfCultists();
        this.Animators = new List<Animator>();
        SetupCharacterPrefabs();
        foreach (Transform child in this.transform)
        {
            this.Animators.Add(child.GetComponent<Animator>());
        }
    }

    public void Attack(BattlefieldContext context)
    {
        Debug.Log("abilities found: " + EnemyParty.Abilities.Count);
        int index = Random.Range(0, EnemyParty.Abilities.Count);
        Debug.Log("Index: " + index);
        this.EnemyParty.Abilities[index].Activate(context);
    }

    public void Animate(string animation)
    {
        foreach (Animator animator in Animators)
        {
            animator.SetTrigger(animation);
        }
    }

    void OnDrawGizmosSelected()
    {

#if UNITY_EDITOR
        Gizmos.color = Color.red;
        Debug.Log(this.transform.position);
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
