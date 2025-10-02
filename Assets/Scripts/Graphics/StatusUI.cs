using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class StatusUI : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _timer, _effect;
    [SerializeField]
    private Image _image;
    public void Setup(Status status)
    {
        _image.sprite = status.Info.Sprite;
        _timer.text = status.Duration + "t";
        _effect.text = status.Description;
    }
}
