using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class suitVoice : MonoBehaviour
{
    AudioSource audioSource;

    public AudioClip[] melee;
    public AudioClip [] heal;
    public AudioClip[] lowHealth;
    public AudioClip[] noSpark;
    public AudioClip[] noWater;
    public AudioClip[] waterFrozen;
    public AudioClip[] waterThawed;
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

    public void playMelee()
    {
        audioSource.Stop();
        int i = Random.Range(0, melee.Length);
        audioSource.PlayOneShot(melee[i]);
    }

    public void playHeal()
    {
        audioSource.Stop();
        int i = Random.Range(0, heal.Length);
        audioSource.PlayOneShot(heal[i]);
    }

    public void playLowHealth()
    {
        audioSource.Stop();
        int i = Random.Range(0, lowHealth.Length);
        audioSource.PlayOneShot(lowHealth[i]);
    }

    public void playNoSpark()
    {
        audioSource.Stop();
        int i = Random.Range(0, noSpark.Length);
        audioSource.PlayOneShot(noSpark[i]);
    }

    public void playNoWater()
    {
        audioSource.Stop();
        int i = Random.Range(0, noWater.Length);
        audioSource.PlayOneShot(noWater[i]);
    }

    public void playWaterFrozen()
    {
        audioSource.Stop();
        int i = Random.Range(0, waterFrozen.Length);
        audioSource.PlayOneShot(waterFrozen[i]);
    }

    public void playWaterThawed()
    {
        audioSource.Stop();
        int i = Random.Range(0, waterThawed.Length);
        audioSource.PlayOneShot(waterThawed[i]);
    }

    public void playSparkRestored()
    {
        audioSource.Stop();
        int i = Random.Range(0, sparkRestored.Length);
        audioSource.PlayOneShot(sparkRestored[i]);
    }

    public void playWaterRestored()
    {
        audioSource.Stop();
        int i = Random.Range(0, waterRestored.Length);
        audioSource.PlayOneShot(waterRestored[i]);
    }

    public void playNewGun()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(newgun);
    }

    public void playArenaComplete()
    {
        audioSource.Stop();
        int i = Random.Range(0, arenaComplete.Length);
        audioSource.PlayOneShot(arenaComplete[i]);
    }

    public void playDeath()
    {
        audioSource.Stop();
        int i = Random.Range(0, death.Length);
        audioSource.PlayOneShot(death[i]);
    }
}
