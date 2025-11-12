using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "HungerMadness", menuName = "Scriptable Objects/Events/HungerMadness")]
public class HungerMadness : EventSO
{

    private int selectedCharacter = -1;
    public override bool CheckOptionAvailability(int selectedOption)
    {
        if (selectedCharacter == -1)
        //get one random character in the party
            selectedCharacter = Random.Range(0, GameManager.i.Characters.Count);
        Debug.Log(selectedCharacter);
        Debug.Log(GameManager.i.Characters.Count);
        this.options[1] = Tools.Parametrize(this.options[1], new System.Collections.Generic.Dictionary<string, string> { { "%character%", GameManager.GetCharacter(selectedCharacter).characterName } });
        switch (selectedOption)
        {
            case 1:
                return GameManager.InventoryContains("Human Flesh") != -1;
            default:
                return true;
        }
    }


    public override void EncounterOptions(int selectedOption)
    {
        Debug.Log(selectedOption);
        switch (selectedOption)
        {
            ///Feed them
            case 0:
                int index = GameManager.InventoryContains("Human Flesh");
                GameManager.GetItem(index).Eat(GameManager.GetCharacter(selectedCharacter));
                GameManager.i.Inventory[index] = null;
                break;

            ///Remove them
            case 1:
                GameManager.i.Characters.Remove(GameManager.GetCharacter(selectedCharacter));
                Debug.Log(GameManager.i.Characters.Count);
                break;
        }
    }
}
