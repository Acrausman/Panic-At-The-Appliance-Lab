using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitboxSpin : MonoBehaviour
{
    public AudioClip clip;
    AudioSource audioSource;
    public float triggerDelay = 0.8f;
    public float amountToRotate = 240;
    public float spinTime = 3;
    private float elapsedTime;
    public Quaternion baseRot;
    public Quaternion endRot;
    BoxCollider collider;
    public bool spinning;
    void Start()
    {
        spinning = false;
        elapsedTime = 0;
        //baseRot = Quaternion.Euler(0, transform.rotation.y, 0);
        //endRot = Quaternion.Euler(0,transform.rotation.y + amountToRotate,0);
        collider = GetComponent<BoxCollider>();
        collider.enabled = false;
        audioSource = GetComponent<AudioSource>();

    }

    private void Update()
    {
        if(spinning)
        {
            elapsedTime += 1 * Time.deltaTime;
            float percentageComplete = elapsedTime / spinTime;

            transform.localRotation = Quaternion.Lerp(baseRot, endRot, percentageComplete);
        }
        else
        {
            transform.localRotation = baseRot;
        }

    }


    public void spin()
    {
        StartCoroutine(spinDelay());
    }

    IEnumerator spinDelay()
    {
        yield return new WaitForSeconds(triggerDelay);
        audioSource.PlayOneShot(clip);
        collider.enabled = true;
        spinning = true;
        yield return new WaitForSeconds(spinTime + 0.5f);
        collider.enabled = false;
        spinning = false;
        elapsedTime = 0;
        transform.rotation = baseRot;
    }
}
