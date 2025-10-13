using UnityEngine;
using System;

public class CharacterGraphics : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer _sprite, _spirit;

    private CharacterBrain _character;

    [SerializeField]
    private Animator _animator;

    [SerializeField]
    private SpriteRenderer _foodSprite;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.Setup(false, null);
    }

    public void AttackAnimation(string animationType)
    {
        this._animator.SetTrigger(animationType);
    }

    public void EatAnimation(Item item)
    {
        _foodSprite.sprite = item.Sprite;
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

    public void Setup(bool isEnemy, Sprite sprite)
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
                    this._sprite.sprite = _character.Sprite;
                    this._spirit.sprite = _character.Sprite;
                }
            }
            else
            {
                this._spirit.sprite = sprite;
                this._sprite.sprite = sprite;
            }


            this._animator = GetComponent<Animator>();
        }
        catch (Exception e)
        {
            //Debug.Log(e);
        }
    }
}
