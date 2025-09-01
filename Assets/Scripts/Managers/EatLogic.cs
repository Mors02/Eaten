using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;
using System.Collections;

public class EatLogic : MonoBehaviour
{
    [SerializeField]
    private Button _button;

    [SerializeField]
    private EnemyManager _em;

    /// <summary>
    /// Party graphics that contains the characters that can move
    /// </summary>
    [SerializeField]
    private Transform _party;

    private Vector2 _returnPosition;    


    public void Start()
    {
        this._em.EnemyParty._onStatsChange.AddListener(ShowButton);
    }

    public void Eat()
    {
        if (GameManager.i.SelectedCharacter == -1)
            return;
        //get random enemy
        Transform enemy = _em.EnemyPositions[UnityEngine.Random.Range(0, _em.EnemyPositions.Count)];

        CharacterMovement movement;
        Animator animator;

        //move selected character to position
        foreach (Transform child in _party)
        {
            if (child.name == GameManager.i.SelectedCharacter.ToString())
            {
                movement = child.GetComponentInChildren<CharacterMovement>();
                _returnPosition = child.parent.transform.position + child.transform.localPosition;

                Debug.Log(_returnPosition);
                animator = child.GetComponent<Animator>();

                movement.enabled = true;
                float time = movement.SetPosition(enemy.position);

                //animator.GetCurrentAnimatorStateInfo(0).normalizedTime
                //remove enemy
                enemy.GetComponent<CharacterMovement>().WillDieIn(time);

                StartCoroutine(GoBack(time, movement, _returnPosition));

                //add eating bonuses
                Debug.Log("This bitch eated");
                GameManager.i.Characters[GameManager.i.SelectedCharacter].EatEnemy(_em.EnemyParty);


            }
        }
    }

    private IEnumerator GoBack(float time, CharacterMovement movement, Vector2 position)
    {
        while (true)
        {
            yield return new WaitForSeconds(time);
            movement.enabled = true;
            movement.SetPosition(position);
        }
    }

    public void ShowButton()
    {
        this._button.gameObject.SetActive(GameManager.i.CanEat);
    }
}
