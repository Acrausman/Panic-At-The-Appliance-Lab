using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyData : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip hitSound;
    public float maxHealth = 40;
    public float currHealth;
    public enemySpawner spawner;
    public GameObject deathPrefab;
    //AudioSource audioSource;
    //public List<AudioClip> voiceLines;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null) audioSource = gameObject.AddComponent<AudioSource>();
        currHealth = maxHealth;
        //audioSource = gameObject.GetComponent<AudioSource>();
        
    }

    public void takeDamage(float amount)
    {
        audioSource.PlayOneShot(hitSound);
        currHealth -= amount;
        if (currHealth <= 0)
        {
            if(spawner != null)
            {
                print("Spawner Valid");
                spawner.addCount();
            }
            if (transform.parent != null) Destroy(transform.parent.gameObject);
            else
            {
                Vector3 spawnVect = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                GameObject death = Instantiate(deathPrefab, spawnVect, Quaternion.identity);
                Destroy(gameObject);
            }
               
        }

        //int x = Random.Range(0, voiceLines.Count - 1);

        //audioSource.PlayOneShot(voiceLines[x]);


    }

}
