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
                Debug.Log(GameObject.FindGameObjectWithTag("UIManager").gameObject.name);
                _i.UiManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
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

    [Header("Enemies")]
    public EnemyPartySO cultists;

    public UIManager UiManager;
}
