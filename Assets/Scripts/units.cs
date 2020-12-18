using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class units : MonoBehaviour
{
    // popup text
    private Animator anim;
    public GameObject popupText;
    public float positionPopuptext = 1.6f;

    public string unitName;
    public string tagName;
    public string race;

    public float runSpeed;
    public float walkSpeed;

    private float deathTime;
    public float aquisitionRange;

    public int hitPoints;
    private int maxHitPoint;

    public bool hasStamina = false;
    public float stamina;
    private float maxStamina;
    public float staminaRecovery;
    public float staminaConsume; 
    public GameObject staminaBar;

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

    public void takeDamage(int damage)
    {
        hitPoints -= damage;
        if (hitPoints < 1)
        {
            Debug.Log(gameObject.name + " est mort");
            anim.SetBool("Death", true);
            //Destroy(gameObject);
            //gameObject.SetActive(false);
        }
        StartCoroutine(takeDamageAnimation());
        GameObject prefabPopupText = Instantiate(popupText, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + positionPopuptext, gameObject.transform.position.z), Quaternion.identity);
        prefabPopupText.transform.GetChild(0).GetComponent<TextMesh>().text = "-" + damage.ToString();
        prefabPopupText.transform.parent = gameObject.transform;
        healthBar.GetComponent<SlideBar>().SetValue(hitPoints);
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
        GetComponent<units>().enabled = true;
    }
    // est appelé a la fin de l'anim : détruit l'objet
    public void Destroy()
    {
        Destroy(gameObject);
    }
}
