using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pauseMenu : MonoBehaviour
{
    public static bool isPaused = false;

    public GameObject pauseUI;
    public string mainMenuString = "MainMenu";

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Pause"))
        {
            if (isPaused) Resume();
            else Pause();
        }
    }

    void Pause()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        pauseUI.SetActive(true);
        Time.timeScale = 0;
        isPaused = true;
    }

    public void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pauseUI.SetActive(false);
        Time.timeScale = 1;
        isPaused = false;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(mainMenuString);
    }

    public void ExitProgram()
    {
        Application.Quit();
    }

    
}
