using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickyAttackTornado : StateMachineBehaviour
{
    private Chicky chicky;
    private float timeToLaunch;

    public GameObject tornadoPrefab;
    public float animSpeed;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        chicky = animator.GetComponent<Chicky>();
        chicky.aIPath.canMove = false;
        timeToLaunch = chicky.timeToLaunchTornado;
        animator.speed = 1f;

        // Remet la velocité a zero
        animator.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        chicky.orientation(animator, chicky.direction());

        animator.speed += (animSpeed / timeToLaunch) * Time.deltaTime;
        if (animator.speed >= animSpeed)
        {
            animator.SetTrigger("LaunchTornado");
        }
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Instantiate(tornadoPrefab, animator.transform.position, Quaternion.identity);
        animator.ResetTrigger("LaunchTornado");
        animator.speed = 1f;
    }
}
