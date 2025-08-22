using UnityEngine;
using UnityEngine.EventSystems;

public class Connect : MonoBehaviour, IPointerEnterHandler, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField]
    private GameObject _line; 
    private ConnectManager _cm;
    private CharacterBrain _cb;

    public CharacterBrain Character => _cb;

    private void Start()
    {
        _cm = GameObject.FindGameObjectWithTag("Canvas").GetComponent<ConnectManager>();
        //TODO: should be passed by the gameManager
        this._cb = new Peasant(this.gameObject.name);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _cm.SelectStartingBlock(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_cm.IsClicking() && _cm.CanSelect(gameObject.name))
        {
            _cm.Connect(this);
           
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        this._cm.StopClicking();
    }

    public Ability GetAbility()
    {
        Debug.Log(_cb.Ability.Name);
        return this._cb.Ability;
    }

    
}