using UnityEngine;

[CreateAssetMenu(fileName = "StatusSO", menuName = "Scriptable Objects/Status/StatusSO")]
public class StatusSO : ScriptableObject
{
    [TextArea(3, 3)]
    public string BaseDescription;
    public StatusName Name;
    public Sprite Sprite;
}
