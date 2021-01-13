using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attack : MonoBehaviour
{
    private movement movementScript;

    public GameObject coneAngle;
    public GameObject coneAttack;

    private Animator anim;

    public int damage;
    public int damageBlock;

    public bool isAttacking = false;
    public float attackRate = 2f;
    private float nextAttackTime = 0f;
    public float attackDuration = 0.3f;
    private float nextMovementTime = 0f;

    public List<Collider2D> ennemiesHits;

    // audio
    private int nbAttackAudio = 0;
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
            if (movementScript.state == movement.State.Attack)
            {
                movementScript.state = movement.State.Normal;
            }
            isAttacking = false;
            anim.SetBool("isAttacking", false);
        }
    }

    public void OnAttack()
    {
        if (Time.time >= nextAttackTime && movementScript.state == movement.State.Normal && movementScript.state != movement.State.Block)
        {
            isAttacking = true;
            anim.SetBool("isAttacking", true);
            movementScript.state = movement.State.Attack;

            nextAttackTime = Time.time + attackRate;
            nextMovementTime = Time.time + attackDuration;

            Collider2D[] hitEnnemies = coneAttack.GetComponent<attackCollide>().TriggerList.ToArray();
            foreach (Collider2D ennemy in hitEnnemies)
            {
                ennemy.gameObject.GetComponent<units>().takeDamage(damage, damageBlock, transform.position);
            }
            if (hitEnnemies.Length > 0)
            {
                int nbAudio = nbAttackAudio % 3;
                Debug.Log(nbAudio);
                switch(nbAudio)
                {
                    case 0:
                        FindObjectOfType<AudioManager>().Play("PelleGrave");
                        break;
                    case 1:
                        FindObjectOfType<AudioManager>().Play("PelleMedium");
                        break;
                    case 2:
                        FindObjectOfType<AudioManager>().Play("PelleAigue");
                        break;
                }
                nbAttackAudio++;
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
