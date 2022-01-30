using System;
using UnityEngine;
using UnityEngine.UI;

public class CardItem : MonoBehaviour
{
    private CardTemplate m_CardInfo;

    private void Start()
    {
        
    }

    public void SetCardInfo(CardTemplate cardInfo)
    {
        
        m_CardInfo = cardInfo;
        ConfigView();
    }

    void ConfigView()
    {
        
        gameObject.GetComponent<Image>().sprite = m_CardInfo.image;
    }

    public void HoverEnter()
    {
        transform.LeanScale(new Vector3(1.2f, 1.2f, 1f), 0.2f);
    }

    public void HoverExit()
    {
        transform.LeanScale(Vector3.one, 0.2f);
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
