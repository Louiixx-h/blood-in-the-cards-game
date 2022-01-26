using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchListView : MonoBehaviour
{
    [SerializeField] private GameObject m_ListViewCardsAction;
    [SerializeField] private GameObject m_ListViewCardsMove;

    [SerializeField] private Button m_ButtonAction;
    [SerializeField] private Button m_ButtonMove;

    void Start()
    {
        ShowCardsAction();

        m_ButtonAction.onClick.AddListener(() =>
        {
            ShowCardsAction();
        });
        m_ButtonMove.onClick.AddListener(() =>
        {
            ShowCardsMove();
        });
    }

    public void ShowCardsAction()
    {
        m_ListViewCardsAction.SetActive(true);
        m_ListViewCardsMove.SetActive(false);
    }

    public void ShowCardsMove()
    {
        m_ListViewCardsAction.SetActive(false);
        m_ListViewCardsMove.SetActive(true);
    }
}
