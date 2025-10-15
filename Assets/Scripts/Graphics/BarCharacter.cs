using UnityEngine;
using System;
using UnityEngine.UI;

public class BarCharacter : MonoBehaviour
{
    [SerializeField]
    private bool _static;

    private CharacterBrain _character;

    [SerializeField]
    private Image _image;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!_static)
        {
            int id = Int32.Parse(gameObject.name);
            Debug.Log(id);
            this._character = GameManager.GetCharacter(id);

            // if the character is null means that the party is smaller than the maximum, remove the graphics.
            if (this._character == null)
            {
                this.gameObject.SetActive(false);
            }
            else
            {
                this._image.sprite = _character.Sprite;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
