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
        if (status.Duration >= 0)
            _timer.text = status.Duration + "t";
        else
            //If the value if less then 0 then do not remove it;
            _timer.gameObject.SetActive(false);    
        _effect.text = status.Description;
    }
}
