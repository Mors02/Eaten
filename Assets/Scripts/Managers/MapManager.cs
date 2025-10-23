using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{
    [SerializeField]
    private Transform _locationsParent;

    private List<MapEvent> _locations;
    [SerializeField]
    [Range(1, 5)]
    private int _activeZones;

    [SerializeField]
    private Button _embarkButton;

    [SerializeField]
    private Animator _animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _locations = new List<MapEvent>();
        foreach (Transform child in _locationsParent)
        {
            _locations.Add(child.GetComponent<MapEvent>());
            child.gameObject.SetActive(false);
        }

        this.SelectZones();
    }

    /// <summary>
    /// Select the zones to be active in the map
    /// </summary>
    public void SelectZones()
    {
        //activate X random zones
        for (int i = 0; i < _activeZones; i++)
        {
            int index = -1;
            //loop through the indices until i find a place that is not already selected
            do
            {
                index = Random.Range(0, _locations.Count);
            } while (_locations[index].gameObject.activeInHierarchy);
            //make it visible
            _locations[index].gameObject.SetActive(true);
        }
    }

    public void SetEvent(MapEvent mapEvent)
    {
        GameManager.i.SelectedEvent = mapEvent.Event;
        foreach (MapEvent e in _locations)
        {
            e.Highlight(false);
        }
        this._embarkButton.interactable = true;
        mapEvent.Highlight(true);

    }

    public void Embark()
    {
        _animator.SetTrigger("Exit");
        Debug.Log(_animator.GetCurrentAnimatorStateInfo(0).length);
        Invoke("ChangeScene", 0.5f);
    }

    public void ChangeScene()
    {
        if (GameManager.i.SelectedEvent.type == MapEventType.Encounter)
            SceneManager.LoadScene("EncounterScene");
        else
            SceneManager.LoadScene("CombatScene");
    }
}
