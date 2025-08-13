using UnityEngine;
using UnityEngine.EventSystems;

public class Connect : MonoBehaviour, IPointerEnterHandler, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField]
    private GameObject _line; 
    private ConnectManager _cm;

    private void Start()
    {
        _cm = GameObject.FindGameObjectWithTag("Canvas").GetComponent<ConnectManager>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _cm.SelectStartingBlock(this.transform);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_cm.IsClicking() && !_cm.AlreadySelected(gameObject.name))
        {
            Debug.Log("Can add " + gameObject.name);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        this._cm.StopClicking();
    }

    
}