using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryTrigger : MonoBehaviour
{
    public string nextScene;
    void OnTriggerEnter(Collider other)
    {
        //print(other.gameObject.name);
        if(other.CompareTag("Player"))
        {
            other.gameObject.GetComponentInParent<playerData>().storeForNextLevel();
            SceneManager.LoadScene(nextScene);
        }
        
    }
}
