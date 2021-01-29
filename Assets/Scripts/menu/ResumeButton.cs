using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResumeButton : MonoBehaviour
{

    public void resume()
    {
        PauseMenu resume = FindObjectOfType<PauseMenu>();
        resume.OnPause();
    }
}
