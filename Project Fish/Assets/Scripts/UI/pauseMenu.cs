using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Cinemachine;
using TMPro;

public class pauseMenu : MonoBehaviour
{
    public static bool isPaused = false;

    public CinemachineVirtualCamera vCam;

    public Slider volumeSlider;

    public Slider fovSlider;
    public TextMeshProUGUI fovText;
    public float baseFOV = 60;

    public GameObject pauseUI;
    public GameObject mainPauseUI;
    public GameObject settingsUI;
    public GameObject videoSettingsUI;
    public GameObject audioSettingsUI;
    public GameObject gameplaySettingsUI;

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
        mainPauseUI.SetActive(true);
        settingsUI.SetActive(false);
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

    public void Settings()
    {
        mainPauseUI.SetActive(false);
        settingsUI.SetActive(true);
    }

    public void backToPause()
    {
        mainPauseUI.SetActive(true);
        settingsUI.SetActive(false);
    }

    public void Video()
    {
        settingsUI.SetActive(false);
        videoSettingsUI.SetActive(true);
    }

    public void Audio()
    {
        settingsUI.SetActive(false);
        audioSettingsUI.SetActive(true);
    }

    public void Gameplay()
    {

    }

    public void backToSettings()
    {
        videoSettingsUI.SetActive(false);
        audioSettingsUI.SetActive(false);
        settingsUI.SetActive(true);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(mainMenuString);
    }

    public void ExitProgram()
    {
        Application.Quit();
    }

    public void ChangeFOV()
    {
        float sliderValue = fovSlider.value + 0.5f;
        float newFOV = baseFOV * sliderValue;
        float displayFOV = ((int)newFOV);
        fovText.text = "Field of View: " + displayFOV.ToString();
        vCam.m_Lens.FieldOfView = newFOV;
    }

    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
    }
    
}
