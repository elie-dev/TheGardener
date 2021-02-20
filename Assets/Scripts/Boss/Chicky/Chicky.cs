using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Chicky : MonoBehaviour
{
    [HideInInspector] public Transform playerPosition;
    [HideInInspector] public AIPath aIPath;
    [HideInInspector] public Rigidbody2D rbChicky;

    public float dashSpeed = 5f;
    public float dashAttackRange = 2f;
    public int dashAttackDamage = 40;
    public int dashAttackDamageBlock = 0;
    public Vector3 attackOffset;
    private Vector3 attackDir;

    public LayerMask attackMask;

    // Start is called before the first frame update
    void Awake()
    {
        aIPath = GetComponent<AIPath>();
        playerPosition = GameObject.Find("Leaf").transform;
        rbChicky = GetComponent<Rigidbody2D>();
    }

    public Vector2 direction()
    {
        return new Vector2(playerPosition.position.x - transform.position.x, playerPosition.position.y - transform.position.y).normalized;
    }

    public void orientation(Animator anim, Vector2 direction)
    {
        anim.SetFloat("Horrizontal", direction.x);
        anim.SetFloat("Vertical", direction.y);
    }

    /*
         
    */
    public bool attackDash(Vector2 direction)
    {
        attackDir = new Vector2(transform.position.x + (direction.x / 10f), transform.position.y + (direction.y / 10f));
        Collider2D colInfo = Physics2D.OverlapCircle(attackDir, dashAttackRange, attackMask);
        if (colInfo != null)
        {
            Debug.Log("bool true");
            colInfo.GetComponent<units>().takeDamage(dashAttackDamage, dashAttackDamageBlock, transform.position);
            return true;
            
        } else
        {
            Debug.Log("bool false");
            return false;
        }
    }

    void OnDrawGizmosSelected()
    {
        Vector3 pos = attackDir;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;

        Gizmos.DrawWireSphere(pos, dashAttackRange);
    }
}
