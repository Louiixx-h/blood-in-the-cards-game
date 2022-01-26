using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardSelected : MonoBehaviour
{
    [SerializeField] private GameObject m_CardSelectedAction;
    [SerializeField] private GameObject m_CardSelectedMove;

    [SerializeField] private Button m_DiscardCardAction;
    [SerializeField] private Button m_DiscardCardMove;

    public delegate void DiscardCardDelegate(CardTemplate cardInfo);
    public event DiscardCardDelegate OnDiscardCard;

    void Start()
    {
        m_CardSelectedAction.SetActive(false);
        m_CardSelectedMove.SetActive(false);

        m_DiscardCardAction.onClick.AddListener(() =>
        {
            OnDiscardCard.Invoke(m_CardSelectedAction.GetComponent<CardItem>().GetCardInfo());
            m_CardSelectedAction.SetActive(false);
        });
        m_DiscardCardMove.onClick.AddListener(() =>
        {
            OnDiscardCard.Invoke(m_CardSelectedMove.GetComponent<CardItem>().GetCardInfo());
            m_CardSelectedMove.SetActive(false);
        });
    }

    public void SaveCardSelected(CardTemplate cardInfo)
    {
        CardItem cardOne = m_CardSelectedAction.GetComponent<CardItem>();
        CardItem cardTwo = m_CardSelectedMove.GetComponent<CardItem>();
        
        if (m_CardSelectedAction.activeSelf == false)
        {
            if (cardInfo.type.Equals(EnumTypeCard.ACTION))
            {
                cardOne.setCardInfo(cardInfo);
                m_CardSelectedAction.SetActive(true);
            }
        }
        if (m_CardSelectedMove.activeSelf == false)
        {
            if (cardInfo.type.Equals(EnumTypeCard.MOVE))
            {
                cardTwo.setCardInfo(cardInfo);
                m_CardSelectedMove.SetActive(true);
            }
        }
    }
}
