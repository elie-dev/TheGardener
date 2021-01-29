using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPause = false;
    public GameObject pauseMenuUI;

    private void Awake()
    {
        
    }

    public void OnPause()
    {
        if (gameIsPause)
        {
            Resume();
        } else
        {
            Pause();
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPause = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPause = true;
    }
}
