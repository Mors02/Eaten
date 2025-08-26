using UnityEngine;

public class Oldman : CharacterBrain
{
    public Oldman(string id) : base(id)
    {
        this._character = GameAssets.i.Oldman;

        this.SetupCharacter();
    }

    public Oldman() : base()
    {
        this._character = GameAssets.i.Oldman;

        this.SetupCharacter();
    }
}
