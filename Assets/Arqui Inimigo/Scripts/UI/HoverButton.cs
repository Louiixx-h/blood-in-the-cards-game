using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoverButton : MonoBehaviour
{
    Button m_Button;
    public Color m_FromColor;
    public Color m_ToColor;

    private void Start()
    {
        m_Button = gameObject.GetComponent<Button>();    
    }

    public void HoverEnter()
    {
        LeanTween.value(m_Button.gameObject, setColorCallback, m_ToColor, m_FromColor, 0.2f);
    }

    public void HoverExit()
    {
        LeanTween.value(m_Button.gameObject, setColorCallback, m_FromColor, m_ToColor, 0.2f);
    }

    private void setColorCallback(Color c)
    {
        m_Button.image.color = c;

        var tempColor = m_Button.image.color;
        tempColor.a = 1f;
        m_Button.image.color = tempColor;
    }
}
