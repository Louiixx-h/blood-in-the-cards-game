using System;
using UnityEngine;
using UnityEngine.UI;

public class CardItem : MonoBehaviour
{
    private CardTemplate m_CardInfo;

    public void setCardInfo(CardTemplate cardInfo)
    {
        m_CardInfo = cardInfo;
        ConfigView();
    }

    void ConfigView()
    {
        gameObject.GetComponent<Image>().sprite = m_CardInfo.image;
    }

    public void DeleteCard()
    {
        Destroy(gameObject);
    }

    public CardTemplate GetCardInfo()
    {
        return m_CardInfo;
    }
}
