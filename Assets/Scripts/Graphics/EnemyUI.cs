using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
public class EnemyUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    [SerializeField]
    private TMP_Text _name, _health;
    [SerializeField]
    private Slider _slider;
    [SerializeField]
    private EnemyManager _em;
    public void OnPointerEnter(PointerEventData eventData)
    {
        GameAssets.i.UiManager.SetupEnemy(this._em.EnemyParty);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GameAssets.i.UiManager.Clean();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this._em.EnemyParty._onStatsChange.AddListener(SetupUI);
        this.SetupUI();
    }

    public void SetupUI()
    {
        this._name.text = this._em.EnemyParty.Name;
        this._health.text = this._em.EnemyParty.CurrentHP + "/" + this._em.EnemyParty.MaxHP;
        this._slider.value = this._em.EnemyParty.CurrentHP / this._em.EnemyParty.MaxHP;
    }
}
