using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTornade : MonoBehaviour
{
    private GameObject playerObj = null;
    public Vector3 wantedPositon;         //La position désirée
    public float speed = 10f;            //La vitesse de déplacement si on utilise MoveToward
    public float damping = 1f;            //Le facteur du lerp
    //damping = 0 -> L'ojet ne se déplacera pas
    //damping = 1000 -> L'objet ira à la position instantanement
    public int damage = 10;
    public float lifeTime;
    private float timer;

    private void Start () {
        if (playerObj == null)
             playerObj = GameObject.Find("Hero");
        
        wantedPositon = transform.position; //Pour que l'objet soit à sa place initiale dans la scene
        timer = Time.time + lifeTime;
    }
    
    private void Update() {

        wantedPositon = playerObj.transform.position;

        transform.position = Vector3.MoveTowards(transform.position,wantedPositon,speed*Time.deltaTime);

//        Debug.Log(playerObj.transform.position);

        if (Time.time > timer)
        {
            Destroy(gameObject);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D Collision)
    {
        //Debug.Log("test");
        if (Collision.gameObject.layer == 9 || Collision.gameObject.layer == 10)
        {
            Debug.Log("Tornage inflige" + damage);
            Collision.gameObject.GetComponent<units>().takeDamage(damage, damage, transform.position);
            Destroy(gameObject);
        }
        else if (Collision.gameObject.layer == 11)
        {
            //Destroy(gameObject);
        }
    }
}