using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Audio;

public class enemyBehaviorWalk : MonoBehaviour
{
    static GameObject target;
    public float moveSpeed = 10;
    public float detectionDistance = 2;
    public float attackDamage = 10;
    public float attackRange = 0.5f;
    public float attackCooldown = 1f;

    Animator animator;
    AudioSource audioSource;
    public AudioClip walkSound;
    public AudioClip attackSound;
    public List<AudioClip> aggroLines;

    bool readyToAttack;

    public bool idle = true;
    void Awake()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        target = GameObject.FindGameObjectWithTag("PlayerDetection");
        animator = gameObject.GetComponent<Animator>();
        readyToAttack = true;
    }

    void Update()
    {
        if (Vector3.Distance(this.transform.position, target.transform.position) < detectionDistance)
        {
            idle = false;
            int x = Random.Range(0, aggroLines.Count - 1);
            audioSource.PlayOneShot(aggroLines[x]);
        }
        if(!idle && Vector3.Distance(this.transform.position, target.transform.position) > attackRange )
        {
            updateMovement();
        }
        else if(!idle && Vector3.Distance(this.transform.position, target.transform.position) <= attackRange)
        {
            attackPlayer();
        }
        
    }

    public void updateMovement()
    {
        animator.SetBool("Attacking", false);
        animator.SetBool("Running", true);
        audioSource.clip = (walkSound);
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, moveSpeed * Time.deltaTime);
        transform.LookAt(target.transform);
    }

    public void attackPlayer()
    {
        if(readyToAttack)
        {
            audioSource.Stop();
            animator.SetBool("Attacking", true);
            animator.SetBool("Running", false);
            transform.LookAt(target.transform);
            readyToAttack = false;
            StartCoroutine(attackDelay());
        }
    }

    IEnumerator attackDelay()
    {
        float timeInterval = attackCooldown / 2;
        yield return new WaitForSeconds(timeInterval);
        yield return new WaitForSeconds(timeInterval);
        readyToAttack = true;

    }
}
