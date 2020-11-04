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
        // déplacement
        rbPlayer.velocity = moveInput * speed;



        //verifie l'orientation
        if(moveInput.x == 0 & moveInput.y == 0)
        {
            Debug.Log("immobile");
        } else if (Mathf.Abs(moveInput.x) > Mathf.Abs(moveInput.y))
        {
            Vector3 rotationVector = transform.rotation.eulerAngles;
            if (moveInput.x < 0)
            {
                rotationVector.y = 180;
                Debug.Log("gauche");
            } else
            {
                rotationVector.y = 0;
                Debug.Log("droite");
            }

            transform.rotation = Quaternion.Euler(rotationVector);
        } else
        {
            if (moveInput.y > 0)
            {
                Debug.Log("haut");
            } else
            {
                Debug.Log("bas");
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
}
