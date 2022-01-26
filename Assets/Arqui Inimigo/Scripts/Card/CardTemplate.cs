using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardObject", menuName = "Card/New Card")]
public class CardTemplate: ScriptableObject
{
    public string name;
    public string description;
    public string parameterName;
    public EnumTypeCard type;
    public EnumBody targetBody;
    public EnumMovement movement;
    public Sprite image;
}