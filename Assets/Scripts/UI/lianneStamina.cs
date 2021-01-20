using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class lianneStamina : MonoBehaviour
{
    public Slider slider;
    private float maxHealth;
    private float actualValue;
    public Sprite[] sprites;
    private float HP;
    public GameObject ronce;

    void Start()
    {
        slider = GetComponent<Slider>();

        actualValue = slider.value;
    }

    void Update()
    {
        if (actualValue != slider.value)
        {
            Health();
        }
    }

    public void Health()
    {
        maxHealth = slider.maxValue;
        actualValue = slider.value;
        HP = actualValue * 100 / maxHealth;
        if (HP == 100)
        {
            ronce.GetComponent<Image>().sprite = sprites[0];
        }
        if (HP < 100 && HP >= 85)
        {
            ronce.GetComponent<Image>().sprite = sprites[1];
        }
        if (HP < 85 && HP >= 70)
        {
            ronce.GetComponent<Image>().sprite = sprites[2];
        }
        if (HP < 70 && HP >= 55)
        {
            ronce.GetComponent<Image>().sprite = sprites[3];
        }
        if (HP < 55 && HP >= 40)
        {
            ronce.GetComponent<Image>().sprite = sprites[4];
        }
        if (HP < 40 && HP >= 20)
        {
            ronce.GetComponent<Image>().sprite = sprites[5];
        }
        if (HP < 20 && HP >= 0)
        {
            ronce.GetComponent<Image>().sprite = sprites[6];
        }
    }
}
