using UnityEngine;

public class Item
{
    private ItemSO _item;

    public string EatDescription => _item.DescriptionWhenEaten;
    public string ThrowDescription => _item.DescriptionWhenThrown;

    public string ItemName => _item.Name;

    public Sprite Sprite => _item.Sprite;

    public Item(ItemSO item)
    {
        this._item = item;
    }

    public void Eat(CharacterBrain character)
    {
        this._item.Eat(character);
    }

    public void Throw(EnemyManager enemy)
    {
        this._item.Throw(enemy);
    }

    public void Hire(CharacterBrain character)
    {
        character.RestoreHunger(this._item.HungerRestore * 2);
    }
}
