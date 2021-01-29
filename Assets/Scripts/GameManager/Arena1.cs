using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Arena1 : MonoBehaviour
{
    public GameObject ennemyEasy;
    public GameObject ennemyMedium;
    public GameObject ennemyHard;

    public ListEnnemies listEnnemies;

    private void Awake()
    {
        // Difficultés
        listEnnemies = GetComponent<ListEnnemies>();
        DifficultyManager Difficulty;
        Difficulty = GameObject.Find("DifficultyManager").GetComponent<DifficultyManager>();
        switch ((int)DifficultyManager.Difficulty)
        {
            case 0:
                Instantiate(ennemyEasy, transform.position, Quaternion.identity);
                break;
            case 1:
                Instantiate(ennemyMedium, transform.position, Quaternion.identity);
                break;
            case 2:
                Instantiate(ennemyHard, transform.position, Quaternion.identity);
                break;
        }

        //Audio
        AudioManager audio = FindObjectOfType<AudioManager>();
        audio.StopAll();
        audio.Play("Fight");
    }

    private void CountEnnemy()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (listEnnemies.ennemies.Count == 0)
        {
            //SceneManager.LoadScene("MainMenu");
        }
    }
}
