using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioSystem : MonoBehaviour
{
    public AudioClip defaultClip;

    public float fadeTime;
    private AudioSource track1, track2;
    private bool isPlayingTrack1;

    public static audioSystem instance;

    private void Awake()
    {
        if (instance == null) instance = this;
    }


    private void Start()
    {
        track1 = gameObject.AddComponent<AudioSource>();
        track1.loop = true;
        track2 = gameObject.AddComponent<AudioSource>();
        track2.loop = true;
        isPlayingTrack1 = true;

        switchTrack(defaultClip);
    }


    public void switchTrack(AudioClip clipToPlay)
    {
        StopAllCoroutines();
        StartCoroutine(fadeTrack(clipToPlay));

        isPlayingTrack1 = !isPlayingTrack1;


    }

    public void returnToDefault()
    {
        switchTrack(defaultClip);
    }

    private IEnumerator fadeTrack(AudioClip clipToPlay)
    {
        print("Called");
        float time = 0;
        if (isPlayingTrack1)
        {
            
            track2.clip = clipToPlay;
            track2.Play();
            

            while(time <= fadeTime)
            {
                track2.volume = Mathf.Lerp(0,1,time / fadeTime);
                track1.volume = Mathf.Lerp(1,0, time / fadeTime);
                time += 1* Time.deltaTime;
                yield return null;  
            }

            track1.Stop();
        }
        else
        {
            track1.clip = clipToPlay;
            track1.Play();
            while (time < fadeTime)
            {
                track1.volume = Mathf.Lerp(0, 1, time / fadeTime);
                track2.volume = Mathf.Lerp(1, 0, time / fadeTime);
                time += 1 * Time.deltaTime;
            }

            track2.Stop();
        }
    }
    
}
