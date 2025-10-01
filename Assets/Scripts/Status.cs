using Unity.VisualScripting;
using UnityEngine;

public class Status
{
    private StatusSO _info;
    public StatusSO Info => _info;
    private int _duration;
    public int Duration => _duration;
    private int _value;
    public int Value => _value;
    
    public string Description { get => this.Info.BaseDescription.Replace("%duration%", _duration.ToString()).Replace("%value%", _value.ToString());  }

    public Status(int duration, int value)
    {
        this._duration = duration;
        this._value = value;
    }

    public Status(StatusSO status, int duration, int value)
    {
        this._info = status;
        this._duration = duration;
        this._value = value;
    }

    public void TickDown()
    {
        this._duration--;
    }
}

public enum StatusName
{
    Healing,
    Bleeding,
    Confusion
}
