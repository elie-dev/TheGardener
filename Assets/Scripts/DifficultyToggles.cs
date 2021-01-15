using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyToggles : MonoBehaviour
{
    DifficultyManager DifficultyManager;
    private void Awake()
    {
        DifficultyManager = GameObject.Find("DifficultyManager").GetComponent<DifficultyManager>();
        gameObject.transform.GetChild((int)DifficultyManager.Difficulty).GetComponent<Toggle>().isOn = true;
    }

    #region Difficluty
    public void setEasyDifficulty(bool isOn)
    {
        if (isOn)
        {
            DifficultyManager.Difficulty = DifficultyManager.Difficulties.Easy;
        }
    }

    public void setMediumDifficulty(bool isOn)
    {
        if (isOn)
        {
            DifficultyManager.Difficulty = DifficultyManager.Difficulties.Medium;
        }
    }

    public void setHardDifficulty(bool isOn)
    {
        if (isOn)
        {
            DifficultyManager.Difficulty = DifficultyManager.Difficulties.Hard;
        }
    }
    #endregion
}
