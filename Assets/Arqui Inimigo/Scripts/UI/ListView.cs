using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListView : MonoBehaviour
{
    [SerializeField] private List<CardTemplate> m_Cards;
    [SerializeField] private GameObject m_CardItem;
    [SerializeField] private EnumTypeCard m_TypeList;

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
            GameObject cardItem = Instantiate(m_CardItem, transform);
            CardItem cardItemScript = cardItem.GetComponent<CardItem>();
            cardItemScript.setCardInfo(card);

            cardItem.GetComponent<Button>().onClick.AddListener(delegate
            {
                OnClickCard.Invoke(card);
                m_Cards.Remove(card);
                cardItemScript.DeleteCard();
            });
        }
    }

    public void RecoverCard(CardTemplate cardInfo)
    {
        if (!cardInfo.type.Equals(m_TypeList)) return;
        GameObject cardItem = Instantiate(m_CardItem, transform);
        CardItem cardItemScript = cardItem.GetComponent<CardItem>();
        cardItemScript.setCardInfo(cardInfo);
        m_Cards.Add(cardInfo);

        cardItem.GetComponent<Button>().onClick.AddListener(delegate
        {
            OnClickCard.Invoke(cardInfo);
            m_Cards.Remove(cardInfo);
            cardItemScript.DeleteCard();
        });
    }
}
