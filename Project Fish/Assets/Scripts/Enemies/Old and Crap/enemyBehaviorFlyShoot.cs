using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBehaviorFlyShoot : MonoBehaviour
{
    public bool canAttack;
    public bool isChasing;

    public AudioClip fly;
    public AudioClip attack;

    public List<GameObject> movePoints;
    public float moveSpeed = 10;
    public float delayTime = 1;
    public float timeInterval;

    public float projectileSpeed = 50;
    public float projectileDamage = 10;
    public GameObject projectile;
    public Transform projectilePoint;
    bool readyToAttack = true;

    public int index = 0;
    Vector3 desiredPoint;
    int previousPoint = 0;

    public GameObject target;
    public bool idle = false;
    public bool readyToMove = true;
    Animator animator;
    AudioSource audioSource;

    private void Awake()
    {
        desiredPoint = new Vector3(movePoints[0].transform.position.x, movePoints[0].transform.position.y, movePoints[0].transform.position.z);
        audioSource = gameObject.GetComponent<AudioSource>();
        target = GameObject.FindGameObjectWithTag("PlayerDetection");
        animator = gameObject.GetComponent<Animator>();
        readyToMove = true;
        timeInterval = delayTime;
    }

    void Update()
    {
        if(canAttack)
        {
            /*
            if (Vector3.Distance(transform.position, desiredPoint) > 1)
            {
                transform.position = Vector3.MoveTowards(transform.position, desiredPoint, moveSpeed * Time.deltaTime);
            }
            else if (readyToMove)
            {
                moveToNextPoint();
                readyToMove = false;
            }
            */
            /*else if (!readyToMove) StartCoroutine(attackSequence());*/

            if(timeInterval >= delayTime)
            {
                animator.SetBool("moving", false);
                animator.SetBool("attacking", true);
                attackSequence();
            }
            else
            {
                animator.SetBool("attacking", false);
                timeInterval += 1 * Time.deltaTime;
            }

            transform.LookAt(target.transform);
        }
        else if(isChasing)
        {
            animator.SetBool("attacking", false);
            animator.SetBool("moving", true);
            transform.LookAt(target.transform);
        }
    }



    void moveToNextPoint()
    {
        index = Random.Range(0, movePoints.Count);
        if(index == previousPoint)
        {
            while (index == previousPoint) index = Random.Range(0, movePoints.Count);
        }
        desiredPoint = new Vector3(movePoints[index].transform.localPosition.x, movePoints[index].transform.localPosition.y, movePoints[index].transform.localPosition.z);

        previousPoint = index;
    }

    void attackSequence()
    {
            GameObject newProj = GameObject.Instantiate(projectile, projectilePoint);
            newProj.transform.parent = null;
            newProj.GetComponent<enemyProjectile>().target = target;
            newProj.GetComponent<enemyProjectile>().speed = projectileSpeed;
            newProj.GetComponent<enemyProjectile>().damage = projectileDamage;
            newProj.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(transform.forward.x, transform.forward.y, projectileSpeed));
        timeInterval = 0;

    }

    void attackSound()
    {
        audioSource.PlayOneShot(attack);
    }
}
