using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Animator m_Animator;

    private CardTemplate m_CurrentMove, m_CurrentAction;
    public bool m_IsReady = false;

    public delegate void Action(CardTemplate cardInfo);
    public event Action OnAction;

    public delegate void Move(CardTemplate cardInfo);
    public event Move OnMove;

    public void DoMove()
    {
        print(m_CurrentMove.name);
        m_Animator.SetTrigger(m_CurrentMove.name);
    }

    public void DoAction()
    {
        m_Animator.SetTrigger(m_CurrentAction.name);
        m_Animator.ResetTrigger(m_CurrentAction.name);
    }

    public void SetCard(CardTemplate card)
    {
        if (card.type.Equals(EnumTypeCard.MOVE))
        {
            m_CurrentMove = card;
        }
        else
        {
            m_CurrentAction = card;
        }
    }

    EnumBody calculateaffectedbodypart(EnumBody targeBody, EnumMovement movement)
    {
        if (targeBody == EnumBody.HEAD)
        {
            if (movement == EnumMovement.IDLE) return EnumBody.HEAD;
            if (movement == EnumMovement.LEFT) return EnumBody.NONE;
            if (movement == EnumMovement.RIGHT) return EnumBody.NONE;
            if (movement == EnumMovement.DOWN) return EnumBody.NONE;
            if (movement == EnumMovement.JUMP) return EnumBody.TRUNK;
        }
        else if (targeBody == EnumBody.LEFT_ARM)
        {
            if (movement == EnumMovement.IDLE) return EnumBody.LEFT_ARM;
            if (movement == EnumMovement.LEFT) return EnumBody.NONE;
            if (movement == EnumMovement.RIGHT) return EnumBody.NONE;
            if (movement == EnumMovement.DOWN) return EnumBody.NONE;
            if (movement == EnumMovement.JUMP) return EnumBody.TRUNK;
        }
        return 0;
    }
}
