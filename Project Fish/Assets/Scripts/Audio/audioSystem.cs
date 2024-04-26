using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioSystem : MonoBehaviour
{
    public float maxVolume = 0.5f;

    public AudioClip defaultClip;

    public float fadeTime;
    private AudioSource track1, track2;
    public bool isPlayingTrack1;
    bool isReady = true;
    float timeToReady;
    public static audioSystem instance;

    private void Awake()
    {
        timeToReady = 0;
        isReady = true;
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

    private void Update()
    {
        if(!isReady)
        {
            if (timeToReady < 1) timeToReady += 1 * Time.deltaTime;
            else isReady = true;
        }
    }


    public void switchTrack(AudioClip clipToPlay)
    {
        if(isReady)
        {
            StopAllCoroutines();
            StartCoroutine(fadeTrack(clipToPlay));

            isPlayingTrack1 = !isPlayingTrack1;
        }


    }

    public void returnToDefault()
    {
        //print("Returning to default");
        switchTrack(defaultClip);
    }

    private IEnumerator fadeTrack(AudioClip clipToPlay)
    {
        //print("Fading track");
        float time = 0;
        if (isPlayingTrack1)
        {
            //print("Switch to track 2");
            track2.clip = clipToPlay;
            track2.Play();
            

            while(time < fadeTime)
            {
                float t2Volume = Mathf.Lerp(0, 1, time / fadeTime);
                float t1Volume = Mathf.Lerp(1, 0, time / fadeTime);
                float cl2 = Mathf.Clamp(t2Volume,0,maxVolume);
                float cl1 = Mathf.Clamp(t1Volume, 0, maxVolume);
                track2.volume = cl2;
                track1.volume = cl1;             
                time += 1* Time.deltaTime;
                yield return null;  
            }

            track1.Stop();
        }
        else
        {
            //print("Switch to track 1");
            track1.clip = clipToPlay;
            track1.Play();
            while (time < fadeTime)
            {
                float t1Volume = Mathf.Lerp(0, 1, time / fadeTime);
                float t2Volume = Mathf.Lerp(1, 0, time / fadeTime);
                float cl1 = Mathf.Clamp(t1Volume, 0, maxVolume);
                //print(cl1.ToString());
                float cl2 = Mathf.Clamp(t2Volume, 0, maxVolume);
                track1.volume = cl1;
                track2.volume = cl2;
                time += 1 * Time.deltaTime;
                print(time.ToString());
                
            }

            track2.Stop();
        }

        isReady = false;
    }
    
}
