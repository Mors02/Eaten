using UnityEngine;

public class Coolguy : CharacterBrain
{
    public Coolguy(string id) : base(id)
    {
        this._character = GameAssets.i.Coolguy;

        this.SetupCharacter();
    }

    public Coolguy() : base()
    {
        this._character = GameAssets.i.Coolguy;

        this.SetupCharacter();
    }
}
