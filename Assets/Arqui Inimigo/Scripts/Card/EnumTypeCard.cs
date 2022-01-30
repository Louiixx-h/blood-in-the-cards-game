using System;
using UnityEngine;

[Serializable]
public struct CardInfo
{
    public string name;
    public EnumTypeCard type;
    public EnumBody body;
    public EnumMovement movement;
    public int damage;
    public Sprite image;
}

public enum EnumTypeCard
{
    ACTION,
    MOVE,
}