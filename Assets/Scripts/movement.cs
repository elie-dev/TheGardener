using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class movement : MonoBehaviour
{
    private units unitsScripts;
    private Rigidbody2D rbPlayer;
    private Animator anim;
    private attack attackScript;

    // basic movement
    private Vector2 moveInput;
    private float speed;
    private bool isSprinting = false;
    public float walkSpeed;
    public float sprintSpeed;
    public float staminaSprint;

    // dash
    private bool isDashing = false;
    public float dashSpeed;
    private float dashTime;
    public float startDashTime;
    public float dashRate;
    private float nextDashTime;
    public float staminaDash;
    private Vector3 dashDir;
    private List<Collider2D> dashTriggerList = new List<Collider2D>();
    public GameObject dashEffect;

    // block
    public bool isBlocking = false;

    // etat du personnage
    public State state;
    public enum State
    {
        Normal,
        DodgeRollSliding,
        Attack,
        Block,
    }


    // Start is called before the first frame update
    void Awake()
    {
        rbPlayer = this.GetComponent<Rigidbody2D>();
        anim = this.GetComponent<Animator>();
        attackScript = GetComponent<attack>();
        unitsScripts = GetComponent<units>();
        speed = walkSpeed;       
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(state);
        switch(state)
        {
            case State.Normal:
                move(moveInput, speed);
                orientation();
                changeStaminaSprint();
                break;
            case State.DodgeRollSliding:
                handleDodgeRollSliding();
                break;
            case State.Attack:
                move(new Vector2(0, 0), 0f);
                break;
            case State.Block:
                if (rbPlayer.velocity != Vector2.zero)
                {
                    rbPlayer.velocity -= rbPlayer.velocity / 2;
                }
                break;
        }
    }

    private bool CanMove(Vector3 dir, float distance)
    {
        return Physics2D.Raycast(transform.position, dir, distance).collider == null;
    }

    public bool TryMove(Vector3 baseMoveDir, float distance)
    {
        Vector3 moveDir = baseMoveDir;
        bool canMove = CanMove(moveDir, distance);
        if (!canMove)
        {
            //cannot move diagonnaly
            moveDir = new Vector3(baseMoveDir.x, 0f).normalized;
            canMove = moveDir.x != 0f && CanMove(moveDir, distance);
            if(!canMove)
            {
                //cannot move horizontally
                moveDir = new Vector3(0f, baseMoveDir.y).normalized;
                canMove = moveDir.y != 0f && CanMove(moveDir, distance);
            }
        }

        if (canMove)
        {
            //lastMoveDir = moveDir;
            transform.position += moveDir * distance;
            return true;
        } else
        {
            return false;
        }
    }

    public void move(Vector2 direction ,float speed)
    {
        // déplacement
        rbPlayer.velocity = direction * speed;
    }

    public void orientation()
    {
        //verifie l'orientation
        if (moveInput.x == 0 & moveInput.y == 0)
        {
            anim.SetBool("isMoving", false);
        }
        else if (Mathf.Abs(moveInput.x) > Mathf.Abs(moveInput.y))
        {
            if (moveInput.x < 0)
            {
                // gauche
                anim.SetBool("isMoving", true);
                anim.SetFloat("Horrizontal", -1);
                anim.SetFloat("Vertical", 0);
                attackScript.changeAngleAttack(-90);
            }
            else
            {
                //droite
                anim.SetBool("isMoving", true);
                anim.SetFloat("Horrizontal", 1);
                anim.SetFloat("Vertical", 0);
                attackScript.changeAngleAttack(90);
            }

        }
        else
        {
            if (moveInput.y > 0)
            {
                //haut
                anim.SetBool("isMoving", true);
                anim.SetFloat("Horrizontal", 0);
                anim.SetFloat("Vertical", 1);
                attackScript.changeAngleAttack(180);
            }
            else
            {
                //"bas"
                anim.SetBool("isMoving", true);
                anim.SetFloat("Horrizontal", 0);
                anim.SetFloat("Vertical", -1);
                attackScript.changeAngleAttack(0);
            }
        }
    }

    /// Recupère les input sous forme de vecteur (X, Y)
    /// 
    /// 
    /// <param name="value"></param>
    public void OnMovement(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    public void OnSprint(InputValue value)
    {
        if(isSprinting && !value.isPressed)
        {
            isSprinting = false;
            speed = walkSpeed;
            anim.SetBool("isSprinting", false);
        } else if (value.isPressed && moveInput != Vector2.zero)
        {
            isSprinting = true;
            speed = sprintSpeed;
            anim.SetBool("isSprinting", true);
        }
    }

    public void changeStaminaSprint()
    {
        if (isSprinting)
        {
            if (unitsScripts.stamina == 0)
            {
                isSprinting = false;
                anim.SetBool("isSprinting", false);
                setDefaultSpeed();
            } else
            {
                unitsScripts.staminaConsume = -staminaSprint;
            }
        } else
        {
            unitsScripts.setDefaultRecoveryStamina();
        }
    }

    public void setDefaultSpeed()
    {
        speed = walkSpeed;
    }

    public void OnDash()
    {
        if(state == State.Normal && moveInput != new Vector2(0, 0) && Time.time >= nextDashTime)
        {
            if (unitsScripts.changeStamina(staminaDash))
            {
                isDashing = true;
                state = State.DodgeRollSliding;
                dashDir = new Vector3(moveInput.x, moveInput.y, transform.position.z);
                dashTime = startDashTime;
                nextDashTime = Time.time + dashRate;

                // spawn prefabs effect
                GameObject dashEffectPref = Instantiate(dashEffect, new Vector3(gameObject.transform.position.x - (moveInput.x * 2), gameObject.transform.position.y - (moveInput.y * 2), 1), Quaternion.Euler(0, 0, 90));
                gameObject.layer = 10;
                float angle = Mathf.Atan2(moveInput.x, moveInput.y) * Mathf.Rad2Deg;
                dashEffectPref.transform.Rotate(0, 0, angle * -1);
                dashEffectPref.transform.parent = gameObject.transform;

                dashTriggerList = new List<Collider2D>();
            }
        }
    }

    public void handleDodgeRollSliding()
    {
        if (dashTime <= 0)
        {
            if (isDashing)
            {
                isDashing = false;
                gameObject.layer = 9;
                state = State.Normal;
            }
        } else
        {
            rbPlayer.velocity = dashDir * dashSpeed;
            dashTime -= Time.deltaTime;
        }
    }

    public void OnBlock(InputValue value)
    {
        if (isBlocking && !value.isPressed)
        {
            isBlocking = false;
            unitsScripts.isBlocking = false;
            state = State.Normal;
        }
        else if (value.isPressed)
        {
            isBlocking = true;
            unitsScripts.isBlocking = true;
            unitsScripts.blockDir = new Vector2(anim.GetFloat("Horrizontal"), anim.GetFloat("Vertical"));
            state = State.Block;
        }
    }

}
