using UnityEngine;
using System;

public class CharacterGraphics : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer _sprite;

    private CharacterBrain _character;

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
        }
        catch (Exception e)
        {
            //Debug.Log(e);
        }
            
            
    }
}
