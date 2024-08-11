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
    Rigidbody rigidbody;
    public float timeToLock = 1;
    float currLockTime = 0;
    bool isYLock = false;
    float yLock = 0;
    bool readyToAttack;
    float baseY;

    public bool idle = true;
    void Awake()
    {
        isYLock = false;
        audioSource = gameObject.GetComponent<AudioSource>();
        target = GameObject.FindGameObjectWithTag("PlayerDetection");
        animator = gameObject.GetComponent<Animator>();
        readyToAttack = true;
        baseY = transform.position.y;
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (currLockTime < timeToLock)
        {
            currLockTime += 1 * Time.deltaTime;
        }
        else isYLock = true; yLock = transform.position.y;

        if (Vector3.Distance(this.transform.position, target.transform.position) < detectionDistance)
        {
            if (idle)
            {
                int x = Random.Range(0, aggroLines.Count - 1);
                audioSource.PlayOneShot(aggroLines[x]);
            }
            idle = false;

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
        Vector3 desiredPos = new Vector3(target.transform.position.x, baseY, target.transform.position.z);
        if (isYLock) desiredPos.y = yLock;
        transform.position = Vector3.MoveTowards(transform.position, desiredPos, moveSpeed * Time.deltaTime);
        //rigidbody.AddForce(desiredPos, ForceMode.Impulse);
        transform.LookAt(target.transform);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Physics.IgnoreCollision(collision.collider, gameObject.GetComponent<Collider>());
        }
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
