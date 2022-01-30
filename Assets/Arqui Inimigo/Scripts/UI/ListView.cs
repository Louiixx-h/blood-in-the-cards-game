using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListView : MonoBehaviour
{
    [SerializeField] private List<CardTemplate> m_Cards;
    [SerializeField] private GameObject m_CardItem;
    [SerializeField] private EnumTypeCard m_TypeList;

    int m_MaxCards = 5;

    public delegate void ActionClickCard(CardTemplate cardInfo);
    public event ActionClickCard OnClickCard;

    private void Start()
    {
        SetListView();
    }

    void SetListView()
    {
        foreach (var card in m_Cards)
        {
            GameObject cardObj = Instantiate(m_CardItem, transform);
            CardItem cardItem = cardObj.GetComponent<CardItem>();
            cardItem.SetCardInfo(card);
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
            CardItem cardItem = child.gameObject.GetComponent<CardItem>();
            CardTemplate cardTemplate = cardItem.GetCardInfo();
            child.GetComponent<Button>().onClick.AddListener(() =>
            {
                OnClick(cardTemplate, cardItem);
            });
        }
    }

    public void RecoverCard(CardTemplate card)
    {
        if (!card.type.Equals(m_TypeList) ||
            m_Cards.Contains(card) ||
            m_Cards.Count == m_MaxCards
        ) return;
        
        GameObject cardObject = Instantiate(m_CardItem, transform);
        CardItem cardItem = cardObject.GetComponent<CardItem>();
        cardItem.SetCardInfo(card);
        m_Cards.Add(card);

        AddAllListeners();
    }

    void OnClick(CardTemplate card, CardItem cardItem)
    {
        OnClickCard.Invoke(card);
        m_Cards.Remove(card);
        cardItem.DeleteCard();
        RemoveAllListeners();
    }

    internal bool HasCard()
    {
        if (m_Cards.Count == 0)
        {
            return false;
        }
        else return true;
    }
}
