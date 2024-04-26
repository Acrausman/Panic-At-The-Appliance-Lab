using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitbox : MonoBehaviour
{
    float damageVal;

    void Awake()
    {
        if(GetComponentInParent<enemyBehaviorWalk>() != null)
        {
            damageVal = GetComponentInParent<enemyBehaviorWalk>().attackDamage;
        }
        else if(GetComponentInParent<enemyBehaviorPrinter>() != null)
        {
            damageVal = GetComponentInParent<enemyBehaviorPrinter>().meleeDamage;
        }
        else if(GetComponentInParent<enemyBehaviorBigchills>() != null)
        {
            damageVal = GetComponentInParent<enemyBehaviorBigchills>().meleeDamage;
        }
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //print("Hit");
        if(other.CompareTag("Player"))
        {
            //print("player hit");
            other.GetComponentInParent<playerData>().takeDamage(damageVal, false, true);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //print("player hit");
            collision.gameObject.GetComponentInParent<playerData>().takeDamage(damageVal, false, true);
            collision.gameObject.GetComponent<Rigidbody>().AddForce(transform.position, ForceMode.Impulse);
        }
        /*else
        {
            Physics.IgnoreCollision(collision.collider, gameObject.GetComponent<Collider>());
        }*/
    }

}
