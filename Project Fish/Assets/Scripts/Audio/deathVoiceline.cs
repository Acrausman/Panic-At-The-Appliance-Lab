using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deathVoiceline : MonoBehaviour
{
    AudioSource audioSource;
    public List<AudioClip> voiceLines;

    // Start is called before the first frame update
    void Awake()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        int x = Random.Range(0, voiceLines.Count - 1);
        audioSource.PlayOneShot(voiceLines[x]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
