using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndAnimation : StateMachineBehaviour
{
    public string name;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (
            animator.GetCurrentAnimatorStateInfo(0).IsName(name) &&
            animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f
        )
        {
            animator.ResetTrigger(name);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger(name);
    }
}
