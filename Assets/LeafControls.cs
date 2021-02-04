using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LeafControls : MonoBehaviour
{
    PlayerControls controls;
    Vector2 move;
    
    void awake()
    {
        controls = new PlayerControls();

        controls.gamepade.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
        controls.gamepade.Move.canceled += ctx => move = Vector2.zero;
        
    }

    void Update()
    {
        Vector2 m = new Vector2(move.x, move.y) * Time.deltaTime;
        transform.Translate(m, Space.World);

    }
}