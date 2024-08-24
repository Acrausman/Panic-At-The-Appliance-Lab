using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float maxHealth;
    float currHealth;
    public float detectionDistance = 100;
    public float moveSpeed = 20;
    public float attackDist = 5;
    public float attackDamage = 10;
    public enum enemyState
    {
        idle,
        moving,
        attacking
    }
    public enemyState currState;


    NavMeshAgent agent;
    Animator animator;
    AudioSource audioSource;

    public enemySpawner spawner;
    public GameObject deathPrefab;

    public AudioClip[] aggroLines;

    GameObject player;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponentInChildren<Animator>();
        audioSource = GetComponent<AudioSource>();

        currHealth = maxHealth;
        agent.speed = moveSpeed;
        agent.stoppingDistance = attackDist;
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) <= attackDist)
        {
            attack();
        }
        else
        {
            agent.isStopped = false;
            updateMovement();
            currState = enemyState.moving;
        }


        updateAnimation();
    }

    public virtual void updateMovement()
    {
        //agent.isStopped = false;
        transform.LookAt(player.transform);
        agent.SetDestination(player.transform.position);
    }

    public virtual void attack()
    {
        agent.isStopped = true;
        agent.SetDestination(transform.position);
        currState = enemyState.attacking;
    }

    public void takeDamage(float amount)
    {
        //audioSource.PlayOneShot(hitSound);
        currHealth -= amount;
        if (currHealth <= 0)
        {
            if (spawner != null)
            {
                //print("Spawner Valid");
                spawner.addCount();
            }
            if (transform.parent != null) Destroy(transform.parent.gameObject);
            else
            {
                Vector3 spawnVect = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                GameObject death = Instantiate(deathPrefab, spawnVect, Quaternion.identity);
                Destroy(gameObject);
            }

        }
    }

    void updateAnimation()
    {
        switch(currState)
        {
            case enemyState.idle:
                animator.SetBool("Attacking", false);
                animator.SetBool("Running", false);
                break;

            case enemyState.moving:
                animator.SetBool("Attacking", false);
                animator.SetBool("Running", true);
                break;

            case enemyState.attacking:
                animator.SetBool("Attacking", true);
                animator.SetBool("Running", false);
                break;

            default:
                animator.SetBool("Attacking", false);
                animator.SetBool("Running", false);
                break;
        }
    }
}
