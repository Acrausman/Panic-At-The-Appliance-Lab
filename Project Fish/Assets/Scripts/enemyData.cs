using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyData : MonoBehaviour
{
    public float maxHealth = 40;
    public float currHealth;
    [HideInInspector]public enemySpawner spawner;

    private void Awake()
    {
        currHealth = maxHealth;
        
    }

    public void takeDamage(float amount)
    {
        currHealth -= amount;
        if (currHealth <= 0)
        {
            spawner.count++;
            if (transform.parent != null) Destroy(transform.parent.gameObject);
            else Destroy(gameObject);
        }
    }

}
