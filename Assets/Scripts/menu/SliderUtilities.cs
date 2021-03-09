using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

public class SliderUtilities : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI targetText;
    public AudioMixer mixer;

    public void increasevalue(float value)
    {
        float newValue = slider.value + value;
        if (newValue < slider.minValue)
        {
            slider.value = slider.minValue;
        } else if (newValue > slider.maxValue)
        {
            slider.value = slider.maxValue;
        } else
        {
            slider.value = newValue;
        }
        targetText.SetText((slider.value + 80).ToString());
    }

    public void setInitialValue()
    {
        float value = 0;
        mixer.GetFloat("volume", out value);
        slider.value = value;
    }
}
