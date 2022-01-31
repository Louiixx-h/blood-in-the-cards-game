using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Animator m_Animator;
    [SerializeField] private List<CardTemplate> m_Movements;
    [SerializeField] private List<CardTemplate> m_Actions;
    [SerializeField] private AudioSource m_FireAudioSource;

    float m_SecondsAnimation = 1f;

    public CardTemplate m_CurrentMove;
    public CardTemplate m_CurrentAction;
    public string m_NameEnemy = "Inimigo";

    public delegate void Action(CardTemplate enumBody);
    public event Action OnAction;

    public delegate void AffectedSite(EnumBody body, string name);
    public event AffectedSite OnAffectedSite;

    public void DoAction()
    {
        m_FireAudioSource.Play();
        GamaManager.OnAction(m_NameEnemy);
        OnAction.Invoke(m_CurrentAction);
        StartCoroutine(ActionAnimation());
    }

    IEnumerator ActionAnimation()
    {
        //m_Animator.SetBool(m_CurrentAction.parameterName, true);
        yield return new WaitForSeconds(m_SecondsAnimation);
        //m_Animator.SetBool(m_CurrentAction.parameterName, false);
        yield return null;
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
        EnumBody bodyAffected = CalculateAffectedbodypart(targeBody.targetBody);
        OnAffectedSite.Invoke(bodyAffected, m_NameEnemy);

        yield return null;
    }

    public void CalculateDamage(CardTemplate targeBody)
    {
        DoMove(targeBody);
    }

    EnumBody CalculateAffectedbodypart(EnumBody targeBody)
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

    internal bool HasCard()
    {
        print("m_Movements: " + m_Movements.Count);
        print("m_Actions: " + m_Movements.Count);
        if (m_Movements.Count == 0 && m_Actions.Count == 0)
        {
            return false;
        }
        else return true;
    }

    internal void ChooseCards()
    {
        int indexAction = Random.Range(0, m_Movements.Count);
        m_CurrentMove = m_Movements[indexAction];
        m_Movements.RemoveAt(indexAction);

        int indexMove = Random.Range(0, m_Actions.Count - 1);
        m_CurrentAction = m_Actions[indexMove];
        m_Actions.RemoveAt(indexMove);
    }
}
