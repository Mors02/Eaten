using UnityEngine;
using System.Collections.Generic;
using System;
using Unity.VisualScripting;

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

                instance.Inventory = new Item[]
                {
                    new Item(GameAssets.i.items[0]),
                    new Item(GameAssets.i.items[1]),
                    new Item(GameAssets.i.items[2]),
                    new Item(GameAssets.i.items[3]),
                };

                instance.SelectedCharacter = -1;
                instance.CanEat = false;
                instance.CanPlay = true;

                //for testing purposes
                instance.SelectedEvent = GameAssets.i.publicEvents[0];
            }

            return instance;
        }
    }

    public List<CharacterBrain> Characters;

    public Item[] Inventory;

    public static CharacterBrain GetCharacter(int id)
    {
        if (id >= i.Characters.Count)
        {
            return null;
        }

        return i.Characters[id];
    }

    public static Item GetItem(int id)
    {
        if (id >= i.Inventory.Length)
        {
            return null;
        }

        return i.Inventory[id];
    }

    public int SelectedCharacter;

    public bool CanEat;

    public bool CanPlay;

    public int EnemiesEaten, CharactersDead;

    public EnemyParty ShownEnemy;
    public CharacterBrain ShownCharacter;

    public EventSO SelectedEvent;

    public DroppableTarget Target;

    public static void PrintInventory()
    {
        string s = "Inventory Length: " + GameManager.i.Inventory.Length + "\n";
        for (int i = 0; i < GameManager.i.Inventory.Length; i++)
        {
            Item item = GameManager.i.Inventory[i];
            s += i + ": " + (item != null? item.ItemName : "empty") + "\n";
        }

        Debug.Log(s);
    }
}
