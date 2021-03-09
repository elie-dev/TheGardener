using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class toggle : MonoBehaviour
{

    public string textFalse;
    public string textTrue;
    public TextMeshProUGUI targetText;

    public bool toggleBool = false;

    public void changeText()
    {
        toggleBool = !toggleBool;
        if (toggleBool)
        {
            targetText.SetText(textTrue);
        } else
        {
            targetText.SetText(textFalse);
        }
    }
}
