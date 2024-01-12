using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelManager1 : MonoBehaviour
{
    public string firstScene;

    public Transform[] enemyPosArena1;
    public Transform[] enemyPosArena2;

    public GameObject coffeMaker;
    public GameObject airFryer;

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
}
