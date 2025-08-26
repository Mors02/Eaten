using UnityEngine;

public class Cleric : CharacterBrain
{
    public Cleric(string id) : base(id)
    {
        this._character = GameAssets.i.Cleric;

        this.SetupCharacter();
    }

    public Cleric() : base()
    {
        this._character = GameAssets.i.Cleric;

        this.SetupCharacter();
    }
}
