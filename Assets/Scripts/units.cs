using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class units : MonoBehaviour
{
    // popup text
    private Animator anim;
    public GameObject popupText;
    public float positionPopuptext = 1.6f;

    // info personnage
    public string unitName;
    public string tagName;
    public string race;

    // Mouvement
    public float runSpeed;
    public float walkSpeed;

    private float deathTime;
    public float aquisitionRange;

    // points de vie
    public int hitPoints;
    private int maxHitPoint;

    // stamina
    public bool hasStamina = false;
    public float stamina;
    private float maxStamina;
    public float staminaRecovery;
    public float staminaConsume; 
    public GameObject staminaBar;

    //block
    public bool isBlocking = false;
    public Vector2 blockDir;

    // UI
    public GameObject healthBar;

    // Anim takeDamage
    public Material[] materials;
    private float timeBetweenAnimation = 0.08f;
    private SpriteRenderer sprite;

    void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        setMaxHealth();
        maxHitPoint = hitPoints;
        staminaConsume = staminaRecovery;

        if (hasStamina)
        {
            maxStamina = stamina;
            setMaxStamina();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (hasStamina)
        {
            //Debug.Log(stamina);
            changeStaminaOverTime(staminaConsume);
        }
    }

    public void takeDamage(int damage, int damageBlock, Vector3 attackDir)
    {
        if (isBlocking)
        {
            // verifie la direction du block
            Vector2 direction = new Vector2(attackDir.x - transform.position.x, attackDir.y - transform.position.y);
            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
            {
                if (direction.x < 0)
                {
                    // gauche
                    direction = new Vector2(-1, 0);
                }
                else
                {
                    //droite
                    direction = new Vector2(1, 0);
                }
            } else
            {
                if (direction.y > 0)
                {
                    //haut
                    direction = new Vector2(0, 1);
                }
                else
                {
                    //bas
                    direction = new Vector2(0, -1);
                }
            }
            if (direction == blockDir)
            {
                DamageBlock(damageBlock);
            } else
            {
                DamageBlock(damage);
            }
            
        } else
        {
            Damage(damage);
        }
        if (hitPoints < 1)
        {
            Debug.Log(gameObject.name + " est mort");
            anim.SetBool("Death", true);
            //Destroy(gameObject);
            //gameObject.SetActive(false);
        }
    }

    public void Damage(int damage)
    {
        hitPoints -= damage;
        StartCoroutine(takeDamageAnimation());
        GameObject prefabPopupText = Instantiate(popupText, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + positionPopuptext, gameObject.transform.position.z), Quaternion.identity);
        prefabPopupText.transform.GetChild(0).GetComponent<TextMesh>().text = "-" + damage.ToString();
        prefabPopupText.transform.parent = gameObject.transform;
        healthBar.GetComponent<SlideBar>().SetValue(hitPoints);
        if (hitPoints < 1)
        {
            Debug.Log(gameObject.name + " est mort");
            anim.SetBool("Death", true);
            //Destroy(gameObject);
            //gameObject.SetActive(false);
        }
    }

    public void DamageBlock(int damageBlock)
    {
        hitPoints -= damageBlock;
        StartCoroutine(takeDamageAnimation());
        GameObject prefabPopupText = Instantiate(popupText, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + positionPopuptext, gameObject.transform.position.z), Quaternion.identity);
        prefabPopupText.transform.GetChild(0).GetComponent<TextMesh>().text = "-" + damageBlock.ToString();
        prefabPopupText.transform.parent = gameObject.transform;
        healthBar.GetComponent<SlideBar>().SetValue(hitPoints);
        if (hitPoints < 1)
        {
            Debug.Log(gameObject.name + " est mort");
            anim.SetBool("Death", true);
            //Destroy(gameObject);
            //gameObject.SetActive(false);
        }
    }

    public IEnumerator takeDamageAnimation()
    {
        foreach (Material material in materials)
        {
            sprite.material = material;
            yield return new WaitForSeconds(timeBetweenAnimation);
        }

    }

    public void setMaxHealth()
    {
        healthBar.GetComponent<SlideBar>().SetMaxvalue(hitPoints);
    }

    public void setMaxStamina()
    {
        staminaBar.GetComponent<SlideBar>().SetMaxvalue(maxStamina);
    }

    public bool changeStamina(float staminas)
    {
        if (staminas > stamina)
        {
            return false;
        } else
        {
            stamina -= staminas;
            Debug.Log(stamina);
            staminaBar.GetComponent<SlideBar>().SetValue(stamina);
            return true;
        }
    }

    private void changeStaminaOverTime(float staminas)
    {
        stamina += staminas;
        if (stamina < 0)
        {
            stamina = 0;
        }
        else if (stamina > maxStamina)
        {
            stamina = maxStamina;
        }
        staminaBar.GetComponent<SlideBar>().SetValue(stamina);
    }
    

    public void setDefaultRecoveryStamina()
    {
        staminaConsume = staminaRecovery;
    }

    // La function death est appelé au debut de l'animation de mort pour désactiver les components de l'objet
    public void Death()
    {
        MonoBehaviour[] comps = GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour c in comps)
        {
            c.enabled = false;
        }
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<units>().enabled = true;
    }
    // est appelé a la fin de l'anim : détruit l'objet
    public void Destroy()
    {
        Destroy(gameObject);
    }
}
