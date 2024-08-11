using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyProjectile : MonoBehaviour
{
    public bool isInk = false;
    public GameObject target;
    public float speed;
    public float damage;
    public float timeActive = 5;
    public float count;

    private void Awake()
    {
        count = timeActive;
    }

    private void Update()
    {
        if(count > 0)
        {
            count -= 1 * Time.deltaTime;
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
            target.GetComponentInParent<playerData>().takeDamage(damage, isInk, true);
            Destroy(gameObject);
        }


    }

}
