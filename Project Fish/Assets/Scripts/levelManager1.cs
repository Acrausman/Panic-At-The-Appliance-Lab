using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelManager1 : MonoBehaviour
{
    public GameObject player;

    public string firstScene;

    public Transform[] enemyPosArena1;
    public Transform[] enemyPosArena2;

    public GameObject coffeMaker;
    public GameObject airFryer;

    public Scene[] checkpointSections;
    [HideInInspector]public int currCheckpoint = 0;



    void Start()
    {
        SceneManager.LoadSceneAsync(firstScene,LoadSceneMode.Additive);
    }

    void Update()
    {
        
    }

    void spawnEnemies()
    {

    }

    void despawnEnemies()
    {

    }

    public void respawnPlayer()
    {

        if (currCheckpoint <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            
        }

    }
}
