using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShot : MonoBehaviour
{
    [SerializeField] private float m_MaxLife = 100f;
    [SerializeField] private float m_CurrentLife;

    [SerializeField] private Image m_SpriteLife;

    private float m_HeadShotDamage = 100f;
    private float m_TrunkDamage = 80f;
    private float m_ArmDamage = 25f;
    private float m_LegDamage = 25f;

    private void Start()
    {
        m_CurrentLife = m_MaxLife;
    }

    public void DoDamage(CardInfo cardInfo)
    {
        switch (cardInfo.body)
        {
            case EnumBody.HEAD: HeadShot();
                break;
            case EnumBody.TRUNK: Trunk();
                break;
            case EnumBody.LEFT_ARM: LeftArmShot();
                break;
            case EnumBody.RIGHT_ARM: RightArmShot();
                break;
            case EnumBody.LEFT_LEG: LeftLegShot();
                break;
            case EnumBody.RIGHT_LEG: RightLegShot();
                break;
        }
    }

    public void HeadShot()
    {
        m_CurrentLife -= m_HeadShotDamage;
        m_SpriteLife.fillAmount = m_CurrentLife / 100f;
    }

    public void Trunk()
    {
        m_CurrentLife -= m_TrunkDamage;
        m_SpriteLife.fillAmount = m_CurrentLife / 100f;
    }

    public void LeftArmShot()
    {
        m_CurrentLife -= m_ArmDamage;
        m_SpriteLife.fillAmount = m_CurrentLife / 100f;
    }

    public void RightArmShot()
    {
        m_CurrentLife -= m_ArmDamage;
        m_SpriteLife.fillAmount = m_CurrentLife / 100f;
    }

    public void LeftLegShot()
    {
        m_CurrentLife -= m_LegDamage;
        m_SpriteLife.fillAmount = m_CurrentLife / 100f;
    }

    public void RightLegShot()
    {
        m_CurrentLife -= m_LegDamage;
        m_SpriteLife.fillAmount = m_CurrentLife / 100f;
    }
}
