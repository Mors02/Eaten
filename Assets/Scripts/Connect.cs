using UnityEngine;
using UnityEngine.EventSystems;

public class Connect : MonoBehaviour, IPointerEnterHandler, IPointerUpHandler, IPointerDownHandler
{
    private bool _clicking = false;
    [SerializeField]
    private GameObject _line;
    private GameObject _canvas;

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Clicked on " + gameObject.name);
        _clicking = true;
        GameObject line = GameObject.Instantiate(_line, this.transform.position, Quaternion.identity, this.transform);
        RectTransform t = line.GetComponent<RectTransform>();
        line.transform.position = new Vector3(this.transform.position.x + (t.sizeDelta.x/2), this.transform.position.y - (t.sizeDelta.y/2));
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("Stopped clicking");
        _clicking = false;
    }

    // Update is called once per frame
    void Update()
    {
        //if (_clicking)
            //Debug.Log("Clicking");
    }

    public void Start()
    {
        this._canvas = GameObject.FindGameObjectWithTag("Canvas");
    }
}
