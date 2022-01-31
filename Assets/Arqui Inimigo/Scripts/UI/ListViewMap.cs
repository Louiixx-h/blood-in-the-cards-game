using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListViewMap : MonoBehaviour
{
    [SerializeField] private List<MapTemplate> m_Maps;
    [SerializeField] private GameObject m_MapItem;

    public delegate void ClickMapDelegate(MapTemplate mapInfo);
    public event ClickMapDelegate OnClickMap;

    private void Start()
    {
        SetListView();
    }

    void SetListView()
    {
        foreach (var map in m_Maps)
        {
            GameObject cardObj = Instantiate(m_MapItem, transform);
            MapItem mapItem = cardObj.GetComponent<MapItem>();
            mapItem.SetMapInfo(map);
            AddAllListeners();
        }
    }

    public void IsVisible(bool enabled)
    {
        if (enabled) gameObject.SetActive(true); 
        else gameObject.SetActive(false);
    }

    public void RemoveAllListeners()
    {
        foreach (Transform child in transform)
        {
            child.GetComponent<Button>().onClick.RemoveAllListeners();
        }
    }

    public void AddAllListeners()
    {
        foreach (Transform child in transform)
        {
            MapItem mapItem = child.gameObject.GetComponent<MapItem>();
            child.GetComponent<Button>().onClick.AddListener(() =>
            {
                OnClick(mapItem.GetMapInfo(), mapItem);
            });
        }
    }

    void OnClick(MapTemplate map, MapItem cardItem)
    {
        OnClickMap.Invoke(map);
        RemoveAllListeners();
    }
}
