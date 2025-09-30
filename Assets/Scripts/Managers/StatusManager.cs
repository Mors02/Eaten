using UnityEngine;
using UnityEngine.UI;

public class StatusManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _statSection, _statusSection;
    [SerializeField]
    private Image _AImage, _DImage, _statBackground, _statusBackground;

    [SerializeField]
    private Color _lightColor, _darkColor;

    private bool _isStatsOpen;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _isStatsOpen = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(_isStatsOpen);
        //Debug.Log(Input.GetButton("D"));
       // Debug.Log(Input.GetButton("A"));
        if (_isStatsOpen && Input.GetButton("D"))
        {
            Debug.Log("OPEN STATS");
            ShowStatus();
            return;
        }

        if (!_isStatsOpen && Input.GetButton("A"))
        {
            Debug.Log("OPEN STATUS");
            ShowStats();
        }
    }

    public void ShowStats()
    {
        _AImage.color = Color.white;
        _statSection.SetActive(true);
        _statBackground.color = _lightColor;

        _DImage.color = _lightColor;
        _statusSection.SetActive(false);
        _statusBackground.color = _darkColor;
        _isStatsOpen = true;
    }

    public void ShowStatus()
    {
        _DImage.color = Color.white;
        _statusSection.SetActive(true);
        _statusBackground.color = _lightColor;

        _AImage.color = _lightColor;
        _statSection.SetActive(false);
        _statBackground.color = _darkColor;
        _isStatsOpen = false;
    }

    public void SwitchSection(string section)
    {
        switch (section)
        {
            case "Stat":

                break;
            case "Status":
                
                break;
        }
    }
}
