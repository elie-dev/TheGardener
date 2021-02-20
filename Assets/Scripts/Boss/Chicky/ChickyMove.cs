using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickyMove : StateMachineBehaviour
{
    private Chicky chicky;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        chicky = animator.GetComponent<Chicky>();
        chicky.aIPath.canMove = true;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        chicky.orientation(animator, chicky.direction());
        if (chicky.aIPath.reachedEndOfPath)
        {
            animator.SetBool("DashAttack", true);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
