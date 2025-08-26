using UnityEngine;

public class Dog : CharacterBrain
{
    public Dog(string id) : base(id)
    {
        this._character = GameAssets.i.Dog;

        this.SetupCharacter();
    }

    public Dog() : base()
    {
        this._character = GameAssets.i.Dog;

        this.SetupCharacter();
    }
}
