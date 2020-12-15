using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attack : MonoBehaviour
{
    private movement movementScript;

    public float angleAttack;
    public GameObject coneAngle;
    public GameObject coneAttack;

    private Animator anim;

    public int damage;

    public bool isAttacking = false;
    public float attackRate = 2f;
    private float nextAttackTime = 0f;
    public float attackDuration = 0.3f;
    private float nextMovementTime = 0f;

    public List<Collider2D> ennemiesHits;

    void Awake()
    {
        anim = this.GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        movementScript = GetComponent<movement>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (isAttacking && Time.time >= nextMovementTime)
        {
            movementScript.state = movement.State.Normal;
            isAttacking = false;
            anim.SetBool("isAttacking", false);
        }
    }

    public void OnAttack()
    {
        if (Time.time >= nextAttackTime && movementScript.state == movement.State.Normal)
        {
            isAttacking = true;
            anim.SetBool("isAttacking", true);
            movementScript.state = movement.State.Attack;

            nextAttackTime = Time.time + attackRate;
            nextMovementTime = Time.time + attackDuration;

            Collider2D[] hitEnnemies = coneAttack.GetComponent<attackCollide>().TriggerList.ToArray();
            foreach (Collider2D ennemy in hitEnnemies)
            {
                ennemy.gameObject.GetComponent<units>().takeDamage(damage);
            }
        }
    }

    public void changeAngleAttack(int angle)
    {
        var rotationVector = coneAngle.transform.rotation.eulerAngles;
        rotationVector.z = angle;
        coneAngle.transform.rotation = Quaternion.Euler(rotationVector);
    }
}
