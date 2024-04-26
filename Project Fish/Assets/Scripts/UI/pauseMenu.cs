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

    public Slider uiScaleSlider;
    public RectTransform[] elementsToScale;
    [SerializeField]Vector3[] baseScaleValues;
    //public RectTransform ammo;
    Vector3 baseAmmoScale;
    //public RectTransform crosshair;
    Vector3 baseCrosshairScale;
    public TextMeshProUGUI uiScaleText;
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
    public GameObject gameOver;
    public fadeEffect gameOverFade;
    float fadeTime;

    public string mainMenuString = "MainMenu";

    void Awake()
    {
        isPaused = false;
        //baseScaleValues = new Vector3[elementsToScale.Length];
        for(int i = 0; i < elementsToScale.Length; i++)
        {
            Vector3 scaleToAdd = new Vector3(elementsToScale[i].localScale.x, elementsToScale[i].localScale.y, elementsToScale[i].localScale.z);
            //print(scaleToAdd);
            baseScaleValues[i] = new Vector3(scaleToAdd.x,scaleToAdd.y,scaleToAdd.z);
        }
        ChangeUIScale();

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Pause"))
        {
            playerData data = GameObject.FindGameObjectWithTag("Player").GetComponentInParent<playerData>();
            if (isPaused && ! data.isDead) Resume();
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

    public void death()
    {
        print("Death");
        pauseUI.SetActive(true);
        gameOver.SetActive(true);
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        gameOverFade.fadeIn = true;
        //isPaused = true;
        StartCoroutine(wait(true));
    }

    public void Resume()
    {
        print("Resuming");
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pauseUI.SetActive(false);
        Time.timeScale = 1;
        gameOver.SetActive(false);
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

    public void retry()
    {
        print("Retry");
        //gameOver.GetComponentInChildren<fadeEffect>().fadeIn = true;
        //levelManager1 manager = GameObject.FindGameObjectWithTag("Level Manager").GetComponent<levelManager1>();
        Time.timeScale = 1;
       
        //StartCoroutine(wait(false));
        //gameOverFade.fadeOut = true;
        Resume();
        levelManager1 manager = GameObject.FindGameObjectWithTag("Level Manager").GetComponent<levelManager1>();
        manager.respawnPlayer();
        //manager.player.GetComponent<playerData>().isDead = false;

    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(mainMenuString);
    }

    public void ExitProgram()
    {
        Application.Quit();
    }

    public void ChangeUIScale()
    {
        float sliderValue = uiScaleSlider.value + 0.5f;
        for(int i = 0; i < elementsToScale.Length; i++)
        {
            Vector3 newScaleValue = baseScaleValues[i] * (sliderValue * 1.5f);
            elementsToScale[i].localScale = newScaleValue;
        }
        uiScaleText.text = "UI Scale: " + sliderValue.ToString("F1")+ "x";
        
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

    IEnumerator wait(bool death)
    {
        print("Start wait");
        if (death)
        {
            yield return new WaitForSeconds(gameOverFade.speed + 0.1f);
            Time.timeScale = 0;
        }
        else
        {
            yield return new WaitForSeconds(gameOverFade.speed + 0.1f);
            print("Start Resume");
            Resume();
        }

    }
    
}
