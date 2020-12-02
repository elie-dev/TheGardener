using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class movement : MonoBehaviour
{
    private Rigidbody2D rbPlayer;
    private Animator anim;

    private attack attackScript;

    private Vector2 moveInput;
    private float speed;
    private bool isSprinting = false;
    public float walkSpeed;
    public float sprintSpeed;

    public float slidingSpeed;
    public float slidingDistance;
    private float slideSpeed;
    private Vector3 slideDir;

    public State state;
    public enum State
    {
        Normal,
        DodgeRollSliding,
        Attack,
    }


    // Start is called before the first frame update
    void Awake()
    {
        rbPlayer = this.GetComponent<Rigidbody2D>();
        anim = this.GetComponent<Animator>();
        attackScript = GetComponent<attack>();
        speed = walkSpeed;       
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(state);
        //Debug.Log(state);
        switch(state)
        {
            case State.Normal:
                move(moveInput, speed);
                orientation();
                break;
            case State.DodgeRollSliding:
                handleDodgeRollSliding();
                break;
            case State.Attack:
                move(new Vector2(0, 0), 0f);
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
            //Debug.Log("immobile");
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
        if(isSprinting)
        {
            isSprinting = false;
            speed = walkSpeed;
            anim.SetBool("isSprinting", false);
        } else
        {
            isSprinting = true;
            speed = sprintSpeed;
            anim.SetBool("isSprinting", true);
        }
    }

    public void OnDash()
    {
        if(state == State.Normal && moveInput != new Vector2(0, 0))
        {
            state = State.DodgeRollSliding;
            slideDir = new Vector3(moveInput.x, moveInput.y, transform.position.z);
            slideSpeed = slidingSpeed;
        }
    }

    public void handleDodgeRollSliding()
    {
        rbPlayer.velocity = slideDir * slidingSpeed * Time.deltaTime;

        slideSpeed -= slideSpeed * slidingDistance * Time.deltaTime;
        if (slideSpeed < 5f)
        {
            state = State.Normal;
        }
    }
}
