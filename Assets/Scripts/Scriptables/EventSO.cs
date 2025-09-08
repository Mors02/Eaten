using UnityEngine;

public abstract class EventSO : ScriptableObject
{
    public MapEventType type;
    public string title;

    [Header("Needed if the type is combat")]
    public EnemyPartySO enemyParty;

    [TextArea(4, 10)]
    [Header("Needed if the type is an encounter")]
    public string description;
    public string[] options;

    [Header("Needed if the type is treasure or food")]
    public int PLACEHOLDER_FOR_ITEMS;

    /// <summary>
    /// an abstract function that must be overrided to implement the various options. An int from 0 to options.Count defines the selected option.
    /// </summary>
    public abstract void EncounterOptions(int selectedOption);

    /// <summary>
    /// Checks whether the option is selectable or not
    /// </summary>
    /// <param name="selectedOption">the option that is checked [0,...,otions.Count-1]</param>
    /// <returns>if true, the option can be selected. false otherwise.</returns>
    public abstract bool CheckOptionAvailability(int selectedOption);
}

public enum MapEventType
{
    Combat,
    Treasure,
    Encounter,
    Food
}
