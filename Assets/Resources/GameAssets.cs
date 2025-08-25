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

    [Header("Enemies")]
    public EnemyPartySO cultists;

    public UIManager UiManager;
}
