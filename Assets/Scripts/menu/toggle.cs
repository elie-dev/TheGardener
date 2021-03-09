using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class toggle : MonoBehaviour
{

    public string textFlase;
    public string textTrue;
    public Text targetText;

    public bool toggleBool = false; 

    public void changeText()
    {
        toggleBool = !toggleBool;
        if (toggleBool)
        {
            targetText.text = textTrue;
        } else
        {
            targetText.text = textFlase;
        }
    }
}
