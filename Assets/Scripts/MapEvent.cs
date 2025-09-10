using System.Collections.Generic;
using UnityEngine.UI;

using UnityEngine;

/// <summary>
/// Describe a map event that can happen when creating the map
/// </summary>
public class MapEvent : MonoBehaviour
{
    [SerializeField]
    private EventSO _mapEvent;

    public EventSO Event => _mapEvent;

    [SerializeField]
    private List<EventSO> _overrideEvents;

    [SerializeField]
    private Image _image;

    private MapManager _mm;

    [SerializeField]
    private Button _button;

    void Start()
    {
        this._mapEvent = SelectEvent();

        this._image.sprite = _mapEvent.sprite;

        this._mm = GameObject.FindGameObjectWithTag("Canvas").GetComponent<MapManager>();        
        this._button.onClick.AddListener(delegate { this._mm.SetEvent(this); });
    }

    /// <summary>
    /// Select an event from the pool. Can be overrided for specific locations that can have particular events.
    /// </summary>
    /// <returns>An event from the event list.</returns>
    public virtual EventSO SelectEvent()
    {
        List<EventSO> events = GameAssets.i.publicEvents;

        return events[Random.Range(0, events.Count)];
    }

    public void Highlight(bool Highlight)
    {
        this.transform.localScale = Highlight? new Vector2(1f, 1f) : new Vector2(0.5f, 0.5f);
    }
}
