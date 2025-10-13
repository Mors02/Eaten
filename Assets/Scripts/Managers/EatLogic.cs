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

    [SerializeField]
    private CombatManager _cm;

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
        if (GameManager.i.SelectedCharacter == -1 || !GameManager.i.CanPlay)
            return;

        if (_em.EnemyPositions.Count == 0)
            return;

        int index = UnityEngine.Random.Range(0, _em.EnemyPositions.Count);
        //get random enemy
        Transform enemy = _em.EnemyPositions[index];

        foreach (Transform graphic in _em.EnemyPositions)
        {
            if (graphic != enemy)
            {
                graphic.GetComponent<CharacterGraphics>().InvisibleAnimation(true);
            }
        }

        CharacterMovement movement;
        Animator animator;

        //move selected character to position
        foreach (Transform child in _party)
        {
            if (child.name == GameManager.i.SelectedCharacter.ToString())
            {
                movement = child.GetComponentInChildren<CharacterMovement>();
                _returnPosition = child.parent.transform.position + child.transform.localPosition;

                
                animator = child.GetComponent<Animator>();

                movement.enabled = true;
                float time = movement.SetPosition(enemy.position);
                
                //animator.GetCurrentAnimatorStateInfo(0).normalizedTime
                //remove enemy
                enemy.GetComponent<CharacterMovement>().WillDieIn(time);

                StartCoroutine(GoBack(time, movement, _returnPosition));
                Camera.main.gameObject.GetComponent<ZoomOnEaten>().CenterOn(enemy.position, time);
                //add eating bonuses

                GameManager.i.Characters[GameManager.i.SelectedCharacter].EatEnemy(_em.EnemyParty);

                this._em.EnemyPositions.Remove(enemy);

                GameManager.i.SelectedCharacter = -1;

                GameManager.i.EnemiesEaten++;

                _cm.RemoveAllHighlights();                
            }
        }

        this._cm.EnemyTurn();
    }

    private IEnumerator GoBack(float time, CharacterMovement movement, Vector2 position)
    {
        while (true)
        {
            yield return new WaitForSeconds(time);
            movement.enabled = true;
            movement.SetPosition(position);
            foreach (Transform graphic in _em.EnemyPositions)
            {
                graphic.GetComponent<CharacterGraphics>().InvisibleAnimation(false);
            }
            StopAllCoroutines();
        }
        
    }

    public void ShowButton()
    {
        this._button.gameObject.SetActive(GameManager.i.CanEat);
        GameAssets.i.UiManager.AddToQueue("The enemies look tasty...");
        this._em.EnemyParty._onStatsChange.RemoveListener(ShowButton);
    }
}
