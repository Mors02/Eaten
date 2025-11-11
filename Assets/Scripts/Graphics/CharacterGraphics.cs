using UnityEngine;
using System;
using TMPro;

public class CharacterGraphics : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer _sprite, _spirit, _highlight;

    [SerializeField]
    private SpriteMask _mask;

    private CharacterBrain _character;

    [SerializeField]
    private Animator _animator;

    [SerializeField]
    private SpriteRenderer _foodSprite;

    [SerializeField]
    private TMP_Text _text;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.Setup(false, null, null);
    }

    public void PlayAnimation(string animationType)
    {
        this._animator.SetTrigger(animationType);
    }

    public void EatAnimation(Item item)
    {
        _foodSprite.sprite = item.Sprite;
        _text.text = item.HungerRestore.ToString();
        this._animator.SetTrigger("Consume");
    }

    public void InvisibleAnimation(bool active)
    {
        Color color = _sprite.color;
        //_animator.SetBool("Invisible", active); 
        color.a = active ? 0.1f : 1f;
        _sprite.color = color;
        Debug.Log(this._sprite.color);
    }

    public void Setup(bool isEnemy, Sprite sprite, Sprite shadowSprite)
    {
        try
        {
            if (!isEnemy)
            {
                int id = Int32.Parse(transform.parent.gameObject.name);
                this._character = GameManager.GetCharacter(id);
                // if the character is null means that the party is smaller than the maximum, remove the graphics.
                if (this._character == null)
                {
                    this.gameObject.SetActive(false);
                }
                else
                {
                    SetupSprites();
                }
            }
            else
            {
                //this._spirit.sprite = sprite;
                this._sprite.sprite = sprite;
                this._mask.sprite = sprite;
                this._highlight.sprite = shadowSprite;
            }


            this._animator = GetComponent<Animator>();
        }
        catch (Exception e)
        {
            //Debug.Log(e);
        }
    }

    public void SetupSprites()
    {
         this._sprite.sprite = _character.Sprite;
        //this._spirit.sprite = _character.Sprite;
        this._mask.sprite = _character.Sprite;
        this._highlight.sprite = _character.ShadowSprite;
    }

    public void SetDamage(int damage)
    {
        this._text.text = damage.ToString();
    }
    
    public void Highlight(bool active)
    {
        //_highlight.enabled = active;
        //_highlight.color = new Color(147, 0, 0);

        if (active) _animator.SetTrigger("Highlight");
        this._sprite.sortingLayerName = active? "Overlay" : "Default";
    }
}
