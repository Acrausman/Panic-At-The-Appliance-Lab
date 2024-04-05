using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class cutsceneBehavior : MonoBehaviour
{
    VideoPlayer videoPlayer;
    public string nextScene;
    bool started = false;

    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        StartCoroutine(delay());
    }

    
    void Update()
    {
        if(started)
        {
            if (!videoPlayer.isPlaying || Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene(nextScene);
            }
        }

    }

    IEnumerator delay()
    {
        yield return new WaitForSeconds(1);
        started = true;
    }
}
