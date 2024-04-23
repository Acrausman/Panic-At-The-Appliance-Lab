using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioSystem : MonoBehaviour
{
    public float maxVolume = 0.5f;

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
        track1.volume = maxVolume;
        track1.loop = true;
        track2 = gameObject.AddComponent<AudioSource>();
        track2.volume = maxVolume;
        track2.loop = true;
        isPlayingTrack1 = true;

        track1.clip = defaultClip;
        track1.Play();
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
        float time = 0;
        if (isPlayingTrack1)
        {
            
            track2.clip = clipToPlay;
            track2.Play();
            

            while(time <= fadeTime)
            {
                float one = Mathf.Lerp(0, 1, time / fadeTime);
                float two = Mathf.Lerp(1, 0, time / fadeTime);
                float cOne = Mathf.Clamp(one,0,maxVolume);
                float cTwo = Mathf.Clamp(two, 0, maxVolume);
                track1.volume = cOne;
                track2.volume = cTwo;
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
                float one = Mathf.Lerp(0, 1, time / fadeTime);
                float two = Mathf.Lerp(1, 0, time / fadeTime);
                float cOne = Mathf.Clamp(one, 0, maxVolume);
                float cTwo = Mathf.Clamp(two, 0, maxVolume);
                track1.volume = cOne;
                track2.volume = cTwo;
                time += 1 * Time.deltaTime;
            }

            track2.Stop();
        }
    }
    
}
