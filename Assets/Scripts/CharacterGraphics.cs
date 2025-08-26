using UnityEngine;
using System;

public class CharacterGraphics : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer _sprite;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       try
        {
            int id = Int32.Parse(transform.parent.gameObject.name);
            this._sprite.sprite = GameManager.i.Characters[id].Sprite;
        } 
        catch(Exception e)
        {
            //Debug.Log(e);
        }
            
            
    }
}
