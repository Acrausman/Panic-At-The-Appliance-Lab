using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryTrigger : MonoBehaviour
{
    public string nextScene;
    void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene(nextScene);
    }
}
