using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    //confine cursor
    private void Start()
    {
        //Cursor.lockState = CursorLockMode.Confined;
    }
    //start game
    public void StartGame() => SceneManager.LoadScene("Level 1");

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
}
