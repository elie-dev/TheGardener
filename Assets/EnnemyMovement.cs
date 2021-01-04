using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnnemyMovement : MonoBehaviour
{
    public AIPath aIPath;

    private EnnemyAttack attackState;

    private Animator anim;
    private Rigidbody2D rb;

    public Transform playerPoisition;

    public bool hasReachDestination = false;
    public Vector2 direction;

    public float speed;

    // tornado 
    [Header("tornado")]
    public GameObject tornado;
    public float timeToNextTornadoMin;
    public float timeToNextTornadoMax;
    private float timerTornado;
    private bool isLanchingTornado = false;
    public float tornadoLaunchTime;
    public float tornadoLaunchSpeed;

    public State state;
    public enum State
    {
        Normal,
        Attack,
        RangeAttack,
    }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        aIPath = GetComponent<AIPath>();
        rb = GetComponent<Rigidbody2D>();
        attackState = GetComponent<EnnemyAttack>();
        timerTornado = Time.time + Random.Range(timeToNextTornadoMin, timeToNextTornadoMax);
    }

    // Update is called once per frame
    void Update()
    {
        hasReachDestination = aIPath.reachedEndOfPath;
        direction = aIPath.desiredVelocity;
        if (state != State.Attack)
        {
            orientation();
            if (Time.time > timerTornado && !isLanchingTornado)
            {
                // lancer une tornade 
                Debug.Log("prepare tornade");
                aIPath.canMove = false;
                rb.velocity = Vector2.zero;
                isLanchingTornado = true;
                anim.speed = 2;
                state = State.RangeAttack;
            }
            if (isLanchingTornado)
            {
                direction = new Vector2(playerPoisition.position.x - transform.position.x, playerPoisition.position.y - transform.position.y);
                orientation();
                anim.speed += tornadoLaunchSpeed * Time.deltaTime;
                if (anim.speed > tornadoLaunchTime)
                {
                    isLanchingTornado = false;
                    anim.speed = 1;
                    aIPath.canMove = true;
                    state = State.Normal;
                    timerTornado = Time.time + Random.Range(timeToNextTornadoMin, timeToNextTornadoMax);
                    Debug.Log("lance la tornade");
                }
            }
        }
        if (hasReachDestination && state != State.RangeAttack)
        {
            direction = new Vector2(playerPoisition.position.x - transform.position.x, playerPoisition.position.y - transform.position.y);
            if (state == State.Normal)
            {
                attackState.Attack();
            }
        }
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
    }
}
