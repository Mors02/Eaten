using UnityEngine;

public class Strongman : CharacterBrain
{
    public Strongman(string id) : base(id)
    {
        this._character = GameAssets.i.Strongman;

        this.SetupCharacter();
    }

    public Strongman() : base()
    {
        this._character = GameAssets.i.Strongman;

        this.SetupCharacter();
    }
}
