using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _partyModal, _mapModal;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this._partyModal.SetActive(false);
        this._mapModal.SetActive(false);
    }

    public void SwitchPartyVisual()
    {
        this._partyModal.SetActive(!this._partyModal.activeSelf);
        this._mapModal.SetActive(false);
    }

    public void SwitchMapVisual()
    {
        this._mapModal.SetActive(!this._mapModal.activeSelf);
        this._partyModal.SetActive(false);
    }
}
