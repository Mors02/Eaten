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
        switch (selectedOption)
        {
            ///add it to your party
            case 1:
                GameManager.i.Characters.Add(new Child());
                break;

            ///Eat it
            case 2:
                foreach (CharacterBrain character in GameManager.i.Characters)
                {
                    character.Eat(20);
                }
                break;
        }
    }
}
