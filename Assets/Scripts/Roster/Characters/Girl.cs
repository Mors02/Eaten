using UnityEngine;

public class Girl : CharacterBrain
{
    public Girl(string id) : base(id)
    {
        this._character = GameAssets.i.Girl;

        this.SetupCharacter();
    }

    public Girl() : base()
    {
        this._character = GameAssets.i.Girl;

        this.SetupCharacter();
    }
}
