using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchListView : MonoBehaviour
{
    [SerializeField] private Button m_ButtonAction;
    [SerializeField] private Button m_ButtonMove;

    public delegate void ButtonActionDelegate(bool isEnabled);
    public event ButtonActionDelegate OnClickAction;

    public delegate void ButtonMoveDelegate(bool isEnabled);
    public event ButtonMoveDelegate OnClickMove;

    void Start()
    {
        m_ButtonAction.onClick.AddListener(() =>
        {
            Up();
        });
        m_ButtonMove.onClick.AddListener(() =>
        {
            OnClickAction.Invoke(false);
            OnClickMove.Invoke(true);
        });
    }

    internal void Up()
    {
        OnClickAction.Invoke(true);
        OnClickMove.Invoke(false);
    }

    internal void RemoveAllListeners()
    {
        m_ButtonAction.onClick.RemoveAllListeners();
        m_ButtonMove.onClick.RemoveAllListeners();
    }
}
