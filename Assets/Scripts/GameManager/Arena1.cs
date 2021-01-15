using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arena1 : MonoBehaviour
{
    public GameObject chickyEasy;
    public GameObject chickyMedium;
    public GameObject chickyHard;


    private void Awake()
    {
        DifficultyManager Difficulty;
        Difficulty = GameObject.Find("DifficultyManager").GetComponent<DifficultyManager>();
        switch ((int)DifficultyManager.Difficulty)
        {
            case 0:
                Instantiate(chickyEasy, transform.position, Quaternion.identity);
                break;
            case 1:
                Instantiate(chickyMedium, transform.position, Quaternion.identity);
                break;
            case 2:
                Instantiate(chickyHard, transform.position, Quaternion.identity);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
