using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAOE : MonoBehaviour
{
    public bool isIce = true;
    public float damagePerSecond;
    public float timeActive = 10;
    void Awake()
    {
        StartCoroutine(countdown());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponentInParent<playerData>() != null)
        {
            other.GetComponentInParent<playerData>().sapHealth(damagePerSecond, isIce);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponentInParent<playerData>() != null)
        {
            other.GetComponentInParent<playerData>().stopDot();
        }
    }

    IEnumerator countdown()
    {
        yield return new WaitForSeconds(timeActive);
        GameObject.Destroy(gameObject);
    }
}
