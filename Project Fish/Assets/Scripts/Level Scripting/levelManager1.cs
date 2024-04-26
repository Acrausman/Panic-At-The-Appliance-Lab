using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelManager1 : MonoBehaviour
{
    public bool isFirstLevel = false;

    public GameObject player;
    public GameObject playerObj;

    public string mainScene;
    public string firstScene;

    public Transform[] enemyPosArena1;
    public Transform[] enemyPosArena2;

    public GameObject coffeMaker;
    public GameObject airFryer;

    //public GameObject[] checkpointSections;
    public int currCheckpoint = 0;

    public Vector3 pointToMove;
    public string sceneToLoad;

    void Awake()
    {
        SceneManager.LoadSceneAsync(firstScene,LoadSceneMode.Additive);
        if(!isFirstLevel)playerObj.GetComponent<playerData>().restoreInventory();
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
            Scene main = SceneManager.GetSceneByName(mainScene);
            SceneManager.MoveGameObjectToScene(player, main);
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                if(SceneManager.GetSceneAt(i).name != mainScene)
                {
                    SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(i));
                }
            }
            StartCoroutine(finishRespawn());
        }

    }

    IEnumerator finishRespawn()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Additive);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        Scene reScene = SceneManager.GetSceneByName(sceneToLoad);
        print(reScene.IsValid().ToString());
        print(player.name + " moving to " + reScene.name);
        SceneManager.MoveGameObjectToScene(player, reScene);
        playerObj.transform.localPosition = new Vector3(pointToMove.x, pointToMove.y, pointToMove.z);
        playerObj.GetComponent<playerData>().currHealth = playerObj.GetComponent<playerData>().maxHealth;
        playerObj.GetComponent<playerData>().isDead = false;



    }
}
