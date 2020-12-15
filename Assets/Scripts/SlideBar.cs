using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlideBar : MonoBehaviour
{
    public Slider slider;

    void Awake()
    {
        slider = GetComponent<Slider>();
    }

    public void SetValue(float value)
    {
        slider.value = value;
    }

    public void SetMaxvalue(float maxValue)
    {
        slider.maxValue = maxValue;
        slider.value = maxValue;
    }
}
