using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Animator m_Animator;
    public bool m_IsReady = false;

    private string m_CurrentMove, m_CurrentAction;
    private List<string> movements = new List<string>()
    {
        "idle",
        "move_down",
        "move_left",
        "move_right",
        "jump"
    };
    private List<EnumBody> actions = new List<EnumBody>()
    {
        EnumBody.HEAD,
        EnumBody.TRUNK,
        EnumBody.RIGHT_ARM,
        EnumBody.LEFT_ARM,
        EnumBody.RIGHT_LEG,
        EnumBody.LEFT_LEG
    };

    public void DoMove()
    {
        string move = movements[Random.Range(0, movements.Count-1)];
        movements.Remove(move);
        m_Animator.SetTrigger(m_CurrentMove);
    }

    public void DoAction()
    {
        int index = Random.Range(0, actions.Count - 1);
        EnumBody move = actions[index];
        movements.RemoveAt(index);
        m_Animator.SetTrigger(m_CurrentAction);
        m_Animator.ResetTrigger(m_CurrentAction);
    }
}
