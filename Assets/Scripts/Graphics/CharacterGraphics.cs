using UnityEngine;
using System;

public class CharacterGraphics : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer _sprite;

    private CharacterBrain _character;

    [SerializeField]
    private Animator _animator;

    [SerializeField]
    private SpriteRenderer _foodSprite;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        try
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
            }

            this._animator = GetComponent<Animator>();
        }
        catch (Exception e)
        {
            //Debug.Log(e);
        }
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
}
