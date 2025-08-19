using System.Collections.Generic;
using UnityEngine;

public abstract class Ability
{
    public string Name { get; set; }

    public string BaseDescription { get; set; }

    public string Description { get; set; }

    public abstract void Activate(/*Pass something*/);

    protected CharacterBrain _character;

    public CharacterBrain Character => _character;

    public Ability(string name, string description)
    {
        this.Name = name;
        this.Description = description;
    }

    public abstract Dictionary<string, int> GetSubstitutions();
}
