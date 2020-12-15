using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class units : MonoBehaviour
{
    // popup text
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

    void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
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
            Destroy(gameObject);
            //gameObject.SetActive(false);
        }
        GameObject prefabPopupText = Instantiate(popupText, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + positionPopuptext, gameObject.transform.position.z), Quaternion.identity);
        prefabPopupText.transform.GetChild(0).GetComponent<TextMesh>().text = "-" + damage.ToString();
        prefabPopupText.transform.parent = gameObject.transform;
        healthBar.GetComponent<SlideBar>().SetValue(hitPoints);
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
}
