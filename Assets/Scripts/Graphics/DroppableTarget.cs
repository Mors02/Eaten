using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;

public class DroppableTarget : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private Connect _targetCharacter;

    public CharacterBrain Character { get => _action == ActionType.Eat? _targetCharacter.Character : null; }

    [SerializeField]
    private EnemyManager _targetEnemy;

    public EnemyManager Enemy { get => _action == ActionType.Throw? _targetEnemy : null; }

    [SerializeField]
    private ActionType _action;

    public ActionType Action => _action;

    [SerializeField]
    public void OnPointerEnter(PointerEventData eventData)
    {
        GameManager.i.Target = this;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GameManager.i.Target = null;
    }
}

public enum ActionType
{
    Eat,
    Throw
}
