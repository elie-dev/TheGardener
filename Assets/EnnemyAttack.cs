using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnnemyAttack : MonoBehaviour
{
    public AIPath aIPath;

    private EnnemyMovement movementScript;

    private Animator anim;
    private Rigidbody2D rb;

    public int damage;
    public int damageBlock;

    public bool isAttacking = false;
    public float attackRate = 2f;
    public float nextAttackTime = 0f;
    public float attackDuration = 0.5f;
    public float attackSpeed = 5f;
    private float nextMovementTime = 0f;
    private Vector2 attackDir;

    private bool heroAlreadyHits = false;
    private bool heroIsCollide = false;

    void Awake()
    {
        anim = this.GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        movementScript = GetComponent<EnnemyMovement>();
        rb = GetComponent<Rigidbody2D>();
        aIPath = GetComponent<AIPath>();
    }

    void Update()
    {

        if (isAttacking && Time.time >= nextMovementTime)
        {
            movementScript.state = EnnemyMovement.State.Normal;
            isAttacking = false;
            aIPath.canMove = true;
            heroAlreadyHits = false;
            anim.SetBool("isAttacking", false);
        } else if (isAttacking)
        {
            rb.velocity = attackDir * attackSpeed;
        }
    }

    public void Attack()
    {
        if (Time.time >= nextAttackTime && movementScript.state == EnnemyMovement.State.Normal)
        {
            isAttacking = true;
            anim.SetBool("isAttacking", true);
            movementScript.state = EnnemyMovement.State.Attack;
            aIPath.canMove = false;

            nextAttackTime = Time.time + attackRate;
            nextMovementTime = Time.time + attackDuration;

            attackDir = new Vector2(movementScript.direction.x, movementScript.direction.y);
            if (heroIsCollide)
            {
                damageHero(movementScript.playerPoisition.gameObject);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            heroIsCollide = true;
            if (isAttacking && !heroAlreadyHits)
            {
                damageHero(collision.gameObject);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            heroIsCollide = false;
        }
    }

    private void damageHero(GameObject hero)
    {
        heroAlreadyHits = true;
        hero.GetComponent<units>().takeDamage(damage, damageBlock, transform.position);
    }
}
