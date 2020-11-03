using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class movement : MonoBehaviour
{

    private Rigidbody2D rbPlayer;

    private Vector2 moveInput;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        rbPlayer = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rbPlayer.velocity = moveInput * speed;
    }

    public void OnMovement(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }
}
