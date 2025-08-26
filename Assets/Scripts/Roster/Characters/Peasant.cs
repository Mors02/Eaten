using UnityEngine;

public class Peasant : CharacterBrain
{
    public Peasant(string id) : base(id)
    {
        this._character = GameAssets.i.Peasant;

        this.SetupCharacter();
    }

    public Peasant() : base()
    {
        this._character = GameAssets.i.Peasant;

        this.SetupCharacter();
    }
}
