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

    [SerializeField]
    private Collider2D _collider;

    private int _id;

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

        if (_parentTransform.name != "Drop")
        {
            this._id = Int32.Parse(this._parentTransform.gameObject.name);
            this._item = GameManager.GetItem(_id);    
        }

        if (this._item == null)
        {
            this._childTransform.gameObject.SetActive(false);
            this._collider.enabled = true;
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
                Debug.Log(hit.collider);

                if (hit.collider && hit.collider.gameObject.TryGetComponent(out target))
                {
                    switch (target.Action)
                    {
                        case ActionType.Eat:
                            if (!GameManager.i.CanPlay)
                                break;

                            _item.Eat(target.Character);
                            if (_cm != null)
                                _cm.EatAnimation(target.Character.Id, _item);
                            break;

                        case ActionType.Throw:
                            if (!GameManager.i.CanPlay)
                                break;

                            _item.Throw(target.Enemy);
                            target.Enemy.ThrowAnimation(_item);
                            break;

                        case ActionType.Delete:                            
                            break;
                        case ActionType.Drop:
                            target.ItemSlot.SetItem(_item);
                            break;

                    }
                    //can only do this if you can play or you cant play but you only do inventory management
                    if (GameManager.i.CanPlay || (!GameManager.i.CanPlay && (target.Action == ActionType.Delete || target.Action == ActionType.Drop)))
                    {
                        if (_parentTransform.gameObject.name != "Drop")
                            GameManager.i.Inventory[_id] = null;
                        this._childTransform.gameObject.SetActive(false);
                        this._collider.enabled = true;
                    }
                }
                
                this._childTransform.anchoredPosition = Vector2.zero;


            }
        }

    }

    public void Inactive()
    {
        this._collider.enabled = false;
        this._childTransform.gameObject.SetActive(false);
    }

    /// <summary>
    /// Setup the graphics;
    /// </summary>
    public void SetupItem()
    {
        this._image.sprite = _item.Sprite;
    }

    /// <summary>
    /// Set the current item to the one passed
    /// </summary>
    /// <param name="item"></param>
    public void SetItem(Item item)
    {
        this._collider.enabled = false;
        this._childTransform.gameObject.SetActive(true);
        this._item = item;
        GameManager.i.Inventory[_id] = this._item;
        SetupItem();
    }

}
