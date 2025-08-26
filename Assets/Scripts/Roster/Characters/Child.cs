using UnityEngine;

public class Child : CharacterBrain
{
    public Child(string id) : base(id)
    {
        this._character = GameAssets.i.Child;

        this.SetupCharacter();
    }

    public Child() : base()
    {
        this._character = GameAssets.i.Child;

        this.SetupCharacter();
    }
}
