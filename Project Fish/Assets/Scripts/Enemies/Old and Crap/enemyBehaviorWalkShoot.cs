using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBehaviorWalkShoot : MonoBehaviour
{
    GameObject player;

    public float moveSpeed = 10;
    public float delayTime = 1;
    float timeInterval;

    public float detectionDistance = 200;
    public float attackDistance = 100;
    public float projectileSpeed = 50;
    public float projectileDamage = 10;
    public GameObject projectile;
    public Transform projectilePoint;
    bool readyToAttack;
    bool idle;

    Animator animator;
    AudioSource audioSource;
    public List<AudioClip> aggroLines;
    public List<AudioClip> shootLines;

    public enum enemyState
    {
        idle,
        moving,
        attacking
    }

    public enemyState currState = enemyState.idle; 


    void Awake()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        readyToAttack = true;
        player = GameObject.FindGameObjectWithTag("Player");
        animator = gameObject.GetComponent<Animator>();

        idle = true;
        currState = enemyState.idle;
    }

    
    void Update()
    {

        switch(currState)
        {
            case enemyState.idle:
                if(checkPlayerdist(attackDistance)) currState = enemyState.attacking;
                else if(checkPlayerdist(detectionDistance)) currState = enemyState.moving;
                else animator.SetBool("moving", false); animator.SetBool("attacking", false);
                break;

            case enemyState.moving:
                if (idle) audioSource.PlayOneShot(chooseSound(aggroLines)); idle = false;
                if (checkPlayerdist(attackDistance)) currState = enemyState.attacking;
                else if(checkPlayerdist(detectionDistance)) moveTowardsPlayer();
                else currState = enemyState.idle;
                break;

            case enemyState.attacking:
                if (idle) audioSource.PlayOneShot(chooseSound(aggroLines)); idle = false;
                if (checkPlayerdist(attackDistance)) attackPlayer();
                else if(checkPlayerdist(detectionDistance)) currState = enemyState.moving;
                break;

            default:
                animator.SetBool("moving", false);
                animator.SetBool("attacking", false);
                break;
                
        }
    }

    

    bool checkPlayerdist(float dist)
    {        
        bool check = Vector3.Distance(this.transform.position, player.transform.position) <= dist;
        return check;
    }

    void moveTowardsPlayer()
    {
        Vector3 targetPos = new Vector3(player.transform.position.x, this.transform.position.y, player.transform.position.z);
        transform.LookAt(targetPos);
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
        animator.SetBool("moving", true);
        animator.SetBool("attacking", false);
    }


    void attackPlayer()
    {
        Vector3 targetPos = new Vector3(player.transform.position.x, this.transform.position.y, player.transform.position.z);
        transform.LookAt(targetPos);
        if (readyToAttack)
        {
            audioSource.PlayOneShot(chooseSound(shootLines));
            animator.SetBool("moving", false);
            animator.SetBool("attacking", true);

            GameObject newProj = GameObject.Instantiate(projectile, projectilePoint);
            newProj.transform.parent = null;
            newProj.GetComponent<enemyProjectile>().target = player;
            newProj.GetComponent<enemyProjectile>().speed = projectileSpeed;
            newProj.GetComponent<enemyProjectile>().damage = projectileDamage;
            newProj.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(transform.forward.x, transform.forward.y, projectileSpeed));
            readyToAttack = false;
            StartCoroutine(attackDelay());
        }
        
    }

    AudioClip chooseSound(List<AudioClip> clips)
    {
        int i = Random.Range(0, clips.Count);
        return clips[i];
    }

    IEnumerator attackDelay()
    {
        yield return new WaitForSeconds(delayTime);
        readyToAttack = true;
    }

    

}
