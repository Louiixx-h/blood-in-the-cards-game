using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapItem : MonoBehaviour
{
    MapTemplate m_MapInfo;
    float m_MaxScale = 1.03f;
    float m_TimeToScale = 0.2f;

    public void SetMapInfo(MapTemplate mapInfo)
    {
        m_MapInfo = mapInfo;
        ConfigView();
    }

    void ConfigView()
    {
        gameObject.GetComponent<Image>().sprite = m_MapInfo.m_Sprite;
        gameObject.GetComponentInChildren<Text>().text = m_MapInfo.m_NameMap;
    }

    public void HoverEnter()
    {
        transform.LeanScale(new Vector3(m_MaxScale, m_MaxScale, 1f), m_TimeToScale);
    }

    public void HoverExit()
    {
        transform.LeanScale(Vector3.one, m_TimeToScale);
    }

    public void DeleteMap()
    {
        Destroy(gameObject);
    }

    public MapTemplate GetMapInfo()
    {
        return m_MapInfo;
    }
}
