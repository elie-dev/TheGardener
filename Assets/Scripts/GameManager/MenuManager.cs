using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    private void Start()
    {
        //Audio
        AudioManager audio = FindObjectOfType<AudioManager>();
        audio.StopAll();
        audio.Play("Menu");
    }
}
