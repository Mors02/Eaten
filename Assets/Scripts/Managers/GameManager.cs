using UnityEngine;
using System.Collections.Generic;
using System;

public class GameManager
{
    private static GameManager instance;
    public static GameManager i
    {
        get
        {
            if (instance == null)
            {
                instance = new GameManager();
                instance.Characters = new List<CharacterBrain>
                {
                    new Peasant(),
                    new Dog(),
                    new Oldman(),
                    new Girl(),
                    new Strongman(),
                    //new Wizard(),
                    //new Cleric(),
                    //new Child(),
                    //new Coolguy()
                };
                instance.SelectedCharacter = -1;
                instance.CanEat = false;
            }

            return instance;
        }
    }

    public List<CharacterBrain> Characters;

    public static CharacterBrain GetCharacter(int id)
    {
       

        if (id >= GameManager.i.Characters.Count)
        {
            return null;
        }

        return i.Characters[id];
    }

    public int SelectedCharacter;

    public bool CanEat;
}
