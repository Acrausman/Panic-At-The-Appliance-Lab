using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicTrigger : MonoBehaviour
{
    public AudioClip middleTrack;
    public AudioClip trackToPLay;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            audioSystem.instance.switchTrack(trackToPLay);
        }
    }
}
