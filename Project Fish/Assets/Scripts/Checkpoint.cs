using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public int checkOrder = 1;
    public GameObject spawn;
    GameObject levelManager;
    public string sceneToLoad;
    void Awake()
    {
        levelManager = GameObject.FindGameObjectWithTag("Level Manager");
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            levelManager1 manager = levelManager.GetComponent<levelManager1>();
            manager.currCheckpoint = checkOrder;
            manager.pointToMove = new Vector3(spawn.transform.position.x, spawn.transform.position.y, spawn.transform.position.z);
            manager.sceneToLoad = sceneToLoad;

        }
    }
}
