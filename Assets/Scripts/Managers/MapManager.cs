using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapManager : MonoBehaviour
{
    [SerializeField]
    private Transform _locationsParent;

    private List<Transform> _locations;

    private MapEvent _selectedEvent;

    [SerializeField]
    [Range(1, 5)]
    private int _activeZones;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _locations = new List<Transform>();
        foreach (Transform child in _locationsParent)
        {
            _locations.Add(child);
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
            _locations[Random.Range(0, _locations.Count)].gameObject.SetActive(true);
        }
    }

    public void SetEvent(MapEvent mapEvent)
    {
        GameManager.i.SelectedEvent = mapEvent.Event;
        mapEvent.Highlight();
    }
}
