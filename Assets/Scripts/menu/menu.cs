﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void quit()
    {
        Application.Quit();
    }

    public void loadlevel(string level)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(level);
    }
}
