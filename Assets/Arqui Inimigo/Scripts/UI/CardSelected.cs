using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardSelected : MonoBehaviour
{
    [SerializeField] private GameObject m_CardSelectedAction;
    [SerializeField] private GameObject m_CardSelectedMove;

    [SerializeField] private GameObject m_CardSelectedActionBg;
    [SerializeField] private GameObject m_CardSelectedMoveBg;

    public delegate void DiscardCardDelegate(CardTemplate cardInfo);
    public event DiscardCardDelegate OnDiscardCard;

    void Start()
    {
        m_CardSelectedAction.SetActive(false);
        m_CardSelectedMove.SetActive(false);

        AddListeners();
    }

    public void SaveCardSelected(CardTemplate cardInfo)
    {
        AddListeners();
        CardItem cardOne = m_CardSelectedAction.GetComponent<CardItem>();
        CardItem cardTwo = m_CardSelectedMove.GetComponent<CardItem>();
        
        if (m_CardSelectedAction.activeSelf == false)
        {
            if (cardInfo.type.Equals(EnumTypeCard.ACTION))
            {
                cardOne.SetCardInfo(cardInfo);
                m_CardSelectedAction.SetActive(true);
                m_CardSelectedActionBg.SetActive(false);
            }
        }
        if (m_CardSelectedMove.activeSelf == false)
        {
            if (cardInfo.type.Equals(EnumTypeCard.MOVE))
            {
                cardTwo.SetCardInfo(cardInfo);
                m_CardSelectedMove.SetActive(true);
                m_CardSelectedMoveBg.SetActive(false);
            }
        }
    }

    void AddListeners()
    {
        m_CardSelectedAction.GetComponent<Button>().onClick.AddListener(() =>
        {
            OnDiscardCard.Invoke(m_CardSelectedAction.GetComponent<CardItem>().GetCardInfo());
            m_CardSelectedAction.SetActive(false);
            m_CardSelectedActionBg.SetActive(true);
        });
        m_CardSelectedMove.GetComponent<Button>().onClick.AddListener(() =>
        {
            OnDiscardCard.Invoke(m_CardSelectedMove.GetComponent<CardItem>().GetCardInfo());
            m_CardSelectedMove.SetActive(false);
            m_CardSelectedMoveBg.SetActive(true);
        });
    }

    internal void RemoveAllListeners()
    {
        m_CardSelectedAction.GetComponent<Button>().onClick.RemoveAllListeners();
        m_CardSelectedMove.GetComponent<Button>().onClick.RemoveAllListeners();
    }

    internal void RemoveCards()
    {
        m_CardSelectedAction.SetActive(false);
        m_CardSelectedMove.SetActive(false);
        m_CardSelectedActionBg.SetActive(true);
        m_CardSelectedMoveBg.SetActive(true);
    }
}