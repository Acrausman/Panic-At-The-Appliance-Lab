using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class enemyBehaviorWalk : MonoBehaviour
{
    GameObject target;
    public float moveSpeed = 10;
    public float detectionDistance = 2;
    public float attackRange = 0.5f;

    Animator animator;

    public bool idle = true;
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("PlayerDetection");
        animator = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        if (Vector3.Distance(this.transform.position, target.transform.position) < detectionDistance) idle = false;
        if(!idle && Vector3.Distance(this.transform.position, target.transform.position) > attackRange )
        {
            updateMovement();
        }
        else if(!idle && Vector3.Distance(this.transform.position, target.transform.position) <= attackRange)
        {
            attack();
        }
        
    }

    public void updateMovement()
    {
        animator.SetBool("Attacking", false);
        animator.SetBool("Running", true);
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, moveSpeed * Time.deltaTime);
        transform.LookAt(target.transform);
    }

    public void attack()
    {
        animator.SetBool("Attacking", true);
        animator.SetBool("Running", false);
        transform.LookAt(target.transform);
    }
}
