using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class killZone : MonoBehaviour
{
    
    void Start()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            enemyData data = other.gameObject.GetComponentInParent<enemyData>();
            data.takeDamage(data.currHealth);
        }
        else if(other.gameObject.CompareTag("Player"))
        {
            playerData data = other.gameObject.GetComponentInParent<playerData>();
            data.takeDamage(data.currHealth, false, false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            enemyData data = collision.gameObject.GetComponentInParent<enemyData>();
            data.takeDamage(data.currHealth);
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            playerData data = collision.gameObject.GetComponentInParent<playerData>();
            data.takeDamage(data.currHealth, false, false);
        }
    }
}
