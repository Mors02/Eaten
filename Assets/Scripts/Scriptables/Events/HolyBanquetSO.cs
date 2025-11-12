using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "HolyBanquetSO", menuName = "Scriptable Objects/Events/HolyBanquetSO")]
public class HolyBanquetSO : EventSO
{
    public override bool CheckOptionAvailability(int selectedOption)
    {
        switch (selectedOption)
        {
            default:
                return true;
        }
    }


    public override void EncounterOptions(int selectedOption)
    {
        Debug.Log(selectedOption);
        switch (selectedOption)
        {
            ///Start a fight
            case 0:
                SceneManager.LoadScene("CombatScene");
                break;

            ///Eat at the banquet
            case 1:
                foreach (CharacterBrain character in GameManager.i.Characters)
                {
                    character.RestoreHunger(5);
                }
                Debug.Log("Banquet Eaten");
                break;
        }
    }
}
