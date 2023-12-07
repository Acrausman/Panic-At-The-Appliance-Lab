using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class suitVoice : MonoBehaviour
{
    AudioSource audioSource;

    public AudioClip [] heal;
    public AudioClip[] lowHealth;
    public AudioClip[] noSpark;
    public AudioClip[] noWater;
    public AudioClip[] sparkRestored;
    public AudioClip[] waterRestored;
    public AudioClip newgun;
    public AudioClip[] arenaComplete;
    public AudioClip[] death;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        
    }

    public void playHeal()
    {
        int i = Random.Range(0, heal.Length);
        audioSource.PlayOneShot(heal[i]);
    }

    public void playLowHealth()
    {
        int i = Random.Range(0, lowHealth.Length);
        audioSource.PlayOneShot(lowHealth[i]);
    }

    public void playNoSpark()
    {
        int i = Random.Range(0, noSpark.Length);
        audioSource.PlayOneShot(noSpark[i]);
    }

    public void playNoWater()
    {
        int i = Random.Range(0, noWater.Length);
        audioSource.PlayOneShot(noWater[i]);
    }

    public void playSparkRestored()
    {
        int i = Random.Range(0, sparkRestored.Length);
        audioSource.PlayOneShot(sparkRestored[i]);
    }

    public void playWaterRestored()
    {
        int i = Random.Range(0, waterRestored.Length);
        audioSource.PlayOneShot(waterRestored[i]);
    }

    public void playNewGun()
    {
        audioSource.PlayOneShot(newgun);
    }

    public void playArenaComplete()
    {
        int i = Random.Range(0, arenaComplete.Length);
        audioSource.PlayOneShot(arenaComplete[i]);
    }

    public void playDeath()
    {
        int i = Random.Range(0, death.Length);
        audioSource.PlayOneShot(death[i]);
    }
}
