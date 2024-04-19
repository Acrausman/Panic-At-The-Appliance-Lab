using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneVolume : MonoBehaviour
{
    public string sceneName;

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Scene scene = SceneManager.GetSceneByName(sceneName);
        if(other.CompareTag("Player"))
        {
            GameObject objToAdd = other.transform.root.gameObject;
            SceneManager.MoveGameObjectToScene(objToAdd, scene);
        }
    }
}
