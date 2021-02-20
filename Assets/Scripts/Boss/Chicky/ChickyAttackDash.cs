using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickyAttackDash : StateMachineBehaviour
{
    private Chicky chicky;
    private Vector2 targetPosition;
    private float speed;
    private bool hasInflictDamage = false;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        chicky = animator.GetComponent<Chicky>();
        chicky.aIPath.canMove = false;
        targetPosition = chicky.direction();
        hasInflictDamage = false;

        // on récupère les variables
        speed = chicky.dashSpeed;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        chicky.rbChicky.velocity = targetPosition * speed;
        if (!hasInflictDamage)
        {
            hasInflictDamage = chicky.attackDash(targetPosition);
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("DashAttack", false);
    }
}
