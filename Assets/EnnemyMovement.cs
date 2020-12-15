using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnnemyMovement : MonoBehaviour
{
    public AIPath aIPath;
    private Animator anim;
    private Rigidbody2D rb;

    public Transform playerPoisition;

    public bool hasReachDestination = false;
    public Vector2 direction;

    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        aIPath = GetComponent<AIPath>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        hasReachDestination = aIPath.reachedEndOfPath;
        direction = aIPath.desiredVelocity;
        if (hasReachDestination)
        {
            direction = new Vector2(playerPoisition.position.x - transform.position.x, playerPoisition.position.y - transform.position.y);
        }
        orientation();
        Debug.Log(aIPath.orientation);
    }

    public void orientation()
    {
        //verifie l'orientation
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if (direction.x < 0)
            {
                // gauche
                anim.SetFloat("Horrizontal", -1);
                anim.SetFloat("Vertical", 0);
                //attackScript.changeAngleAttack(-90);
            }
            else
            {
                //droite
                anim.SetFloat("Horrizontal", 1);
                anim.SetFloat("Vertical", 0);
                //attackScript.changeAngleAttack(90);
            }

        }
        else
        {
            if (direction.y > 0)
            {
                //haut
                anim.SetFloat("Horrizontal", 0);
                anim.SetFloat("Vertical", 1);
                //attackScript.changeAngleAttack(180);
            }
            else
            {
                //"bas"
                anim.SetFloat("Horrizontal", 0);
                anim.SetFloat("Vertical", -1);
                //attackScript.changeAngleAttack(0);
            }
        }

        if (hasReachDestination)
        {
            anim.SetBool("isMoving", false);
        } else
        {
            anim.SetBool("isMoving", true);
        }
    }
}
