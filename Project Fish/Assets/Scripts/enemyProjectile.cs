using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyProjectile : MonoBehaviour
{
    public GameObject target;
    public float speed;
    public float damage;
    public float timeActive = 5;
    float count;

    private void Awake()
    {
        count = timeActive;
    }

    private void Update()
    {
        if(timeActive > 0)
        {
            count += 1 * Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            target.GetComponentInParent<playerData>().takeDamage(damage);
        }

        Destroy(gameObject);

    }

}
