using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickyMove : StateMachineBehaviour
{
    private Chicky chicky;
    private bool launchTornado = false;
    private float timeBetweenDashAttack;
    private float timerDashAttack;

    private float rangeLeaf;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        launchTornado = false;
        chicky = animator.GetComponent<Chicky>();
        float random = Random.Range(0.00f, 1.00f);
        if (random <= chicky.tornadoAttackDashAttackRate)
        {
            launchTornado = true;
            Debug.Log("tornade !");
            animator.SetTrigger("PrepareTornado");
        } else
        {
            chicky.aIPath.canMove = true;

            // On set la distance a 0.5 pour eviter au max les obstacles
            rangeLeaf = chicky.aIPath.endReachedDistance;
            chicky.aIPath.endReachedDistance = 0.5f;

            //On set le timer pour réguler les dash
            timeBetweenDashAttack = chicky.timeBetweenDashAttack;
            timerDashAttack = Time.time;
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        chicky.orientation(animator, chicky.aIPath.desiredVelocity.normalized);
        if (timerDashAttack < Time.time - timeBetweenDashAttack)
        {
            chicky.aIPath.endReachedDistance = rangeLeaf;
            if (chicky.aIPath.reachedEndOfPath && !launchTornado)
            {
                animator.SetBool("DashAttack", true);
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("PrepareTornado");
    }
}
