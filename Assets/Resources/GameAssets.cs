using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    private static GameAssets _i;

    public static GameAssets i
    {
        get
        {
            if (_i == null)
            {
                _i = Instantiate(Resources.Load<GameAssets>("GameAssets"));
                GameObject.FindGameObjectWithTag("UIManager").TryGetComponent(out _i.UiManager);
            }

            return _i;
        }
    }

    [Header("Characters")]
    public Character Peasant;
    public Character Dog;
    public Character Oldman;
    public Character Girl;
    public Character Strongman;
    public Character Wizard;
    public Character Cleric;
    public Character Child;
    public Character Coolguy;

    public UIManager UiManager;

    [Header("Events")]
    public EventSO RandomCombat;
    public EventSO LostChild;

    public List<EventSO> publicEvents;

    public List<ItemSO> items;
}
