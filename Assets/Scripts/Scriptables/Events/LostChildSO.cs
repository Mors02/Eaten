using UnityEngine;

[CreateAssetMenu(fileName = "LostChildSO", menuName = "Scriptable Objects/Events/LostChildSO")]
public class LostChildSO : EventSO
{
    public override bool CheckOptionAvailability(int selectedOption)
    {
        switch (selectedOption)
        {
            case 1:
                if (GameManager.i.Characters.Count < 9)
                    return true;
                return false;
            default:
                return true;
        }
    }


    public override void EncounterOptions(int selectedOption)
    {
        Debug.Log(selectedOption);
        switch (selectedOption)
        {
            ///add it to your party
            case 0:
                GameManager.AddCharacter(new Child());
                Debug.Log("Added child");
                break;

            ///Eat it
            case 1:
                foreach (CharacterBrain character in GameManager.i.Characters)
                {
                    character.Eat(20);
                }
                Debug.Log("Child eaten");
                break;
        }
    }
}
