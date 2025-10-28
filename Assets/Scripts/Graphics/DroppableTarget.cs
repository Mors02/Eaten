using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;

public class DroppableTarget : MonoBehaviour
{
    [SerializeField]
    private Connect _targetCharacter;

    [SerializeField]
    private BarCharacter _targetBarCharacter;

    public CharacterBrain Character { get => _action == ActionType.Eat ? _targetCharacter.Character : _targetBarCharacter.Character; }

    [SerializeField]
    private EnemyManager _targetEnemy;

    public EnemyManager Enemy { get => _action == ActionType.Throw? _targetEnemy : null; }

    [SerializeField]
    private ItemUI _itemUI;

    public ItemUI ItemSlot { get => _action == ActionType.Drop ? _itemUI : null; }

    [SerializeField]
    private ActionType _action;

    public ActionType Action => _action;
}

public enum ActionType
{
    Eat,
    Throw,
    Delete,

    Drop,
    Hire
}
