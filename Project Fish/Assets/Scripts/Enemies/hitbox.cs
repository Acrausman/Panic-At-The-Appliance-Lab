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
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.GetComponentInParent<playerData>().takeDamage(damageVal);
        }
    }

}
