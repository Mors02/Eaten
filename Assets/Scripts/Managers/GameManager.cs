using UnityEngine;
using System.Collections.Generic;

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
                    new Wizard(),
                    new Cleric(),
                    new Child(),
                    new Coolguy()
                };
            }

            return instance;
        }
    }

    public List<CharacterBrain> Characters;
}
