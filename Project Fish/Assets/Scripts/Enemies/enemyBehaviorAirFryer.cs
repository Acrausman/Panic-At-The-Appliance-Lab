using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBehaviorAirFryer : MonoBehaviour
{
    public enum enemyState
    {
        ide,
        attacking,
        chasing
    }

    public enemyState currState;

    public GameObject target;

    enemyBehaviorFlyShoot behavior;

    public float detectionDistance = 20;
    public float chaseSpeed = 10;
    public float attackRange = 10;
    void Awake()
    {
        print("Valid " + gameObject.name);
        currState = enemyState.ide;
        behavior = GetComponentInChildren<enemyBehaviorFlyShoot>();
        target = GameObject.FindGameObjectWithTag("Player");
        behavior.canAttack = false;
        behavior.isChasing = false;
    }

    // Update is called once per frame
    void Update()
    {
        print(Vector3.Distance(this.transform.position, target.transform.position).ToString());
        if(Vector3.Distance(this.transform.position, target.transform.position) <= detectionDistance)
        {
            //print("Looking");
            transform.LookAt(target.transform.position);
            if (Vector3.Distance(this.transform.position, target.transform.position) <= attackRange)
            {
                print("Attacking");
                behavior.isChasing = false;
                behavior.canAttack = true;
                currState = enemyState.attacking;
            }
            else
            {
                behavior.canAttack = false;
                behavior.isChasing = true;
                currState = enemyState.chasing;
                //transform.position = Vector3.MoveTowards(transform.position, target.transform.position, chaseSpeed * Time.deltaTime);
            }
        }
        else
        {
            behavior.canAttack = false;
        }
    }
}
