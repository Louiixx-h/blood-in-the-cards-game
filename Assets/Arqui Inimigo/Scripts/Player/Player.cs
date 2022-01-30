using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Animator m_Animator;
    
    CardTemplate m_CurrentMove;
    CardTemplate m_CurrentAction;
    bool m_IsReady = false;
    float m_SecondsAnimation = 1f;
    public string m_NamePlayer = "Jogador";

    public delegate void ActionDelegate(CardTemplate enumBody);
    public event ActionDelegate OnAction;

    public delegate void UseCardsDelegate();
    public event UseCardsDelegate OnUseCards;

    public delegate void AffectedSiteDelegate(EnumBody body, string name);
    public event AffectedSiteDelegate OnAffectedSite;

    public bool IsReady
    {
        get => m_IsReady;
    }

    public void DoAction()
    {
        OnUseCards.Invoke();
        OnAction.Invoke(m_CurrentAction);
        GamaManager.OnAction(m_NamePlayer);
        StartCoroutine(ActionAnimation());
    }

    IEnumerator ActionAnimation()
    {
        //m_Animator.SetBool(m_CurrentAction.parameterName, true);
        yield return new WaitForSeconds(m_SecondsAnimation);
        //m_Animator.SetBool(m_CurrentAction.parameterName, false);
        yield return null;
    }

    public void SetCard(CardTemplate card)
    {
        if (card.type.Equals(EnumTypeCard.MOVE)) m_CurrentMove = card;
        else m_CurrentAction = card;

        if (m_CurrentAction != null && m_CurrentMove != null)
        {
            m_IsReady = true;
        }
    }

    public void RemoveCard(CardTemplate card)
    {
        if (card.type.Equals(EnumTypeCard.ACTION)) m_CurrentAction = null;
        else m_CurrentMove = null;
        m_IsReady = false;
    }

    public void DoMove(CardTemplate targeBody)
    {
        StartCoroutine(MoveAnimation(targeBody));
    }

    IEnumerator MoveAnimation(CardTemplate targeBody)
    {
        string nameAnimtion = m_CurrentMove.parameterName;
        m_Animator.SetBool(nameAnimtion, true);
        
        yield return new WaitForSeconds(m_SecondsAnimation);
        
        m_Animator.SetBool(nameAnimtion, false);
        EnumBody bodyAffected = Calculateaffectedbodypart(targeBody.targetBody);
        OnAffectedSite.Invoke(bodyAffected, m_NamePlayer);
        
        yield return null;
    }

    public void CalculateDamage(CardTemplate targeBody)
    {
        DoMove(targeBody);
    }

    EnumBody Calculateaffectedbodypart(EnumBody targeBody)
    {
        if (targeBody == EnumBody.HEAD)
        {
            if (m_CurrentMove.movement == EnumMovement.IDLE) return EnumBody.HEAD;
            if (m_CurrentMove.movement == EnumMovement.LEFT) return EnumBody.NONE;
            if (m_CurrentMove.movement == EnumMovement.RIGHT) return EnumBody.NONE;
            if (m_CurrentMove.movement == EnumMovement.DOWN) return EnumBody.NONE;
            if (m_CurrentMove.movement == EnumMovement.JUMP) return EnumBody.TRUNK;
        }                    
        else if (targeBody == EnumBody.LEFT_ARM)
        {                    
            if (m_CurrentMove.movement == EnumMovement.IDLE) return EnumBody.LEFT_ARM;
            if (m_CurrentMove.movement == EnumMovement.LEFT) return EnumBody.NONE;
            if (m_CurrentMove.movement == EnumMovement.RIGHT) return EnumBody.NONE;
            if (m_CurrentMove.movement == EnumMovement.DOWN) return EnumBody.NONE;
            if (m_CurrentMove.movement == EnumMovement.JUMP) return EnumBody.TRUNK;
        }                    
        else if (targeBody == EnumBody.RIGHT_ARM)
        {                    
            if (m_CurrentMove.movement == EnumMovement.IDLE) return EnumBody.RIGHT_ARM;
            if (m_CurrentMove.movement == EnumMovement.LEFT) return EnumBody.NONE;
            if (m_CurrentMove.movement == EnumMovement.RIGHT) return EnumBody.LEFT_ARM;
            if (m_CurrentMove.movement == EnumMovement.DOWN) return EnumBody.NONE;
            if (m_CurrentMove.movement == EnumMovement.JUMP) return EnumBody.RIGHT_LEG;
        }                    
        else if (targeBody == EnumBody.LEFT_LEG)
        {                    
            if (m_CurrentMove.movement == EnumMovement.IDLE) return EnumBody.LEFT_LEG;
            if (m_CurrentMove.movement == EnumMovement.LEFT) return EnumBody.RIGHT_LEG;
            if (m_CurrentMove.movement == EnumMovement.RIGHT) return EnumBody.NONE;
            if (m_CurrentMove.movement == EnumMovement.DOWN) return EnumBody.TRUNK;
            if (m_CurrentMove.movement == EnumMovement.JUMP) return EnumBody.NONE;
        }                    
        else if (targeBody == EnumBody.RIGHT_LEG)
        {                    
            if (m_CurrentMove.movement == EnumMovement.IDLE) return EnumBody.RIGHT_LEG;
            if (m_CurrentMove.movement == EnumMovement.LEFT) return EnumBody.NONE;
            if (m_CurrentMove.movement == EnumMovement.RIGHT) return EnumBody.LEFT_LEG;
            if (m_CurrentMove.movement == EnumMovement.DOWN) return EnumBody.TRUNK;
            if (m_CurrentMove.movement == EnumMovement.JUMP) return EnumBody.NONE;
        }                    
        return 0;            
    }
}
