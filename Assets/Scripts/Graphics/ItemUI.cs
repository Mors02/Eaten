using System;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemUI : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler
{

    private bool _clicked;

    private Vector2 _startPosition;

    [SerializeField]
    private RectTransform _parentTransform, _childTransform;

    private Item _item;

    [SerializeField]
    private Image _image;

    [SerializeField]
    private CombatManager _cm;

    public void OnPointerDown(PointerEventData eventData)
    {
        this._clicked = true;

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GameAssets.i.UiManager.SetupItem(_item);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this._clicked = false;
        this._startPosition = this._parentTransform.anchoredPosition;
        //this._childTransform = gameObject.GetComponent<RectTransform>();
        int id = Int32.Parse(this._parentTransform.gameObject.name);
        this._item = GameManager.GetItem(id);

        if (this._item == null)
        {
            this._childTransform.gameObject.SetActive(false);
            return;
        }

        this._cm = GameObject.FindGameObjectWithTag("Canvas").GetComponent<CombatManager>();

        this.SetupItem();
    }

    void Update()
    {

        if (_clicked)
        {
            Debug.Log(Input.GetMouseButton(0));
            if (Input.GetMouseButton(0))
            {
                Debug.Log("Clicking");
                this._childTransform.position = Input.mousePosition;
            }
            else
            {
                this._clicked = false;

                /* RaycastHit2D[] hits = Physics2D.RaycastAll(Input.mousePosition, Vector3.forward);
                 foreach (RaycastHit2D hit in hits)
                 {
                     Debug.Log(hit.collider.gameObject.name);
                 }*/
                RaycastHit2D hit = Physics2D.Raycast(Input.mousePosition, Vector3.forward);
                DroppableTarget target;

                if (hit.collider && hit.collider.gameObject.TryGetComponent<DroppableTarget>(out target))
                {
                    if (target.Action == ActionType.Eat)
                    {
                        _item.Eat(target.Character);
                        _cm.EatAnimation(target.Character.Id, _item);
                    }
                    else
                    {
                        _item.Throw(target.Enemy);
                        target.Enemy.ThrowAnimation(_item);
                    }
                    this._childTransform.gameObject.SetActive(false);
                }

                this._childTransform.anchoredPosition = _startPosition;


            }
        }
        else
        {
            //Debug.Log("Not Clicking");
        }

    }

    public void SetupItem()
    {
        this._image.sprite = _item.Sprite;
    }

}
