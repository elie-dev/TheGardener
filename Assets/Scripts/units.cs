using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class units : MonoBehaviour
{
    public string unitName;
    public string tagName;
    public string race;

    public float runSpeed;
    public float walkSpeed;

    private float deathTime;
    public float aquisitionRange;

    public int hitPoints;

    // UI
    public GameObject healthBar;

    void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        setMaxHealth();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool takeDamage(int damage)
    {
        hitPoints -= damage;
        if (hitPoints < 1)
        {
            //gameObject.SetActive(false);
            Debug.Log(gameObject.name + " est mort");
            gameObject.SetActive(false);
        }
        healthBar.GetComponent<HealthBar>().SetHealth(hitPoints);
        return false;
    }

    public void setMaxHealth()
    {
        healthBar.GetComponent<HealthBar>().SetMaxHealth(hitPoints);
    }
}
