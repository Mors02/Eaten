using UnityEngine;

[CreateAssetMenu(fileName = "StarvingFamily", menuName = "Scriptable Objects/Events/StarvingFamily")]
public class StarvingFamilySO : EventSO
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
        switch (selectedOption)
        {
            ///Go away
            case 0:

                break;

            ///Follow him
            case 1:
                for (int j = 0; j < GameManager.i.Inventory.Length; j++)
                {
                    if (GameManager.GetItem(j) == null)
                        GameManager.i.Inventory[j] = new Item(GameAssets.i.items[3]);
                }
                break;
        }
    }
}
