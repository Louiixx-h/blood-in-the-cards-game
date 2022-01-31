using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MapObject", menuName = "Map/New Map")]
public class MapTemplate : ScriptableObject
{
    public string m_NameMap;
    public Sprite m_Sprite;
}
