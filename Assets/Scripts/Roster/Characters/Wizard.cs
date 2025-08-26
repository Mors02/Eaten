using UnityEngine;

public class Wizard : CharacterBrain
{
    public Wizard(string id) : base(id)
    {
        this._character = GameAssets.i.Wizard;

        this.SetupCharacter();
    }

    public Wizard() : base()
    {
        this._character = GameAssets.i.Wizard;

        this.SetupCharacter();
    }
}
