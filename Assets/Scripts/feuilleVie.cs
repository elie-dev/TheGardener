using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class feuilleVie : MonoBehaviour
{
    public Slider slider;
    private float maxHealth;
    private float actualValue;
    public Sprite[] sprites;
    private float HP;
    public GameObject feuille;

    void Start(){
        slider = GetComponent<Slider>();
        
        actualValue = slider.value;
    }

    void Update(){
        if(actualValue != slider.value){
          Health();  
        }
    }

    public void Health(){
        maxHealth = slider.maxValue;
        actualValue = slider.value;
        HP = actualValue * 100 / maxHealth;
        if(HP == 100){
            feuille.GetComponent<Image>().sprite = sprites[0];
        }
        if(HP < 100 && HP >= 90){
            feuille.GetComponent<Image>().sprite = sprites[1];
        }
        if(HP < 90 && HP >= 80){
            feuille.GetComponent<Image>().sprite = sprites[2];
        }
        if(HP < 80 && HP >= 70){
            feuille.GetComponent<Image>().sprite = sprites[3];
        }
        if(HP < 70 && HP >= 60){
            feuille.GetComponent<Image>().sprite = sprites[4];
        }
        if(HP < 60 && HP >= 50){
            feuille.GetComponent<Image>().sprite = sprites[5];
        }
        if(HP < 50 && HP >= 40){
            feuille.GetComponent<Image>().sprite = sprites[6];
        }
        if(HP < 40 && HP >= 30){
            feuille.GetComponent<Image>().sprite = sprites[7];
        }
        if(HP < 30 && HP >= 20){
            feuille.GetComponent<Image>().sprite = sprites[8];
        }
        if(HP < 20 && HP >= 10){
            feuille.GetComponent<Image>().sprite = sprites[9];
        }
        if(HP < 10 && HP > 0){
            feuille.GetComponent<Image>().sprite = sprites[10];
        }
        if(HP == 0){
            feuille.GetComponent<Image>().sprite = sprites[11];
        }
    }


    
}
