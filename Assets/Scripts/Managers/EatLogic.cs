using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class EatLogic : MonoBehaviour
{
    [SerializeField]
    private Button _button;

    [SerializeField]
    private EnemyManager _em;


    public void Start()
    {

    }

    public void Eat()
    {
        if (GameManager.i.SelectedCharacter == -1)
            return;
        //get random enemy
        Transform enemy = _em.EnemyPositions[Random.Range(0, _em.EnemyPositions.Count)];

        //move selected character to position


        //remove enemy

        //add eating bonuses
    }
}
