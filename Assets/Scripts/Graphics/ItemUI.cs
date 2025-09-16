using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class ItemUI : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler
{

    private bool _clicked;

    public void OnPointerDown(PointerEventData eventData)
    {
          this._clicked = true;
        Debug.Log("CLICKED");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GameAssets.i.UiManager.SetupItem();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this._clicked = false;
    }

    void Update()
    {
        
        if (_clicked)
        {
            Debug.Log(Input.GetMouseButton(0));
            if (Input.GetMouseButton(0))
            {
                Debug.Log("Clicking");
                this.transform.position = Input.mousePosition;
            }
            else
            {
                Debug.Log("Stopped clicking");
                this._clicked = false;
            }
        }
        else
        {
            //Debug.Log("Not Clicking");
        }
        
    }

}
