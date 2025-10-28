using UnityEngine;

public abstract class ItemSO : ScriptableObject
{

    public string Name;

    public int HungerRestore;

    public int Damage;

    public int Heal;

    public Sprite Sprite;

    [TextArea(3, 4)]
    public string DescriptionWhenThrown, DescriptionWhenEaten;

    public abstract void Eat(CharacterBrain character);

    public abstract void Throw(EnemyManager enemy);    
}
