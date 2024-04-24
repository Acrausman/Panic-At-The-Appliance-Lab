using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBehaviorBigchills : MonoBehaviour
{
    GameObject player;
    hitboxSpin spin;

    public float moveSpeed = 10;
    public float delayTime = 1;
    float timeInterval;

    public float detectionDistance = 200;
    public float attackDistance = 100;
    public float projectileSpeed = 50;
    public float projectileDamage = 10;
    public List<GameObject> projectileTypes;
    public List<Transform> projectilePoints;
    bool readyToAttack;

    Animator animator;
    AudioSource audioSource;

    public enum enemyState
    {
        idle,
        moving,
        attacking
    }
    public enum bossAttack
    {
        tongue,
        iceToMeetYou,
        popmissiles
    }

    public List<bossAttack> attackPattern;

    public bossAttack currAttack;
    int patternProg;

    public enemyState currState = enemyState.idle;


    void Awake()
    {
        spin = GetComponentInChildren<hitboxSpin>();
        audioSource = gameObject.GetComponent<AudioSource>();
        readyToAttack = true;
        player = GameObject.FindGameObjectWithTag("Player");
        animator = gameObject.GetComponent<Animator>();

        currState = enemyState.idle;
        currAttack = attackPattern[0];
        patternProg = 0;
    }


    void Update()
    {

        switch (currState)
        {
            case enemyState.idle:
                if (checkPlayerdist(attackDistance)) currState = enemyState.attacking;
                else if (checkPlayerdist(detectionDistance)) currState = enemyState.moving;
                else goToIdle();
                break;

            case enemyState.moving:
                if (checkPlayerdist(attackDistance)) currState = enemyState.attacking;
                else if (checkPlayerdist(detectionDistance)) moveTowardsPlayer();
                else currState = enemyState.idle;
                break;

            case enemyState.attacking:
                if (checkPlayerdist(attackDistance)) attackPlayer();
                else if (checkPlayerdist(detectionDistance)) currState = enemyState.moving;
                break;

            default:
                goToIdle();
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
        goToWalking();
        //animator.SetBool("attacking", false);
    }


    void attackPlayer()
    {
        goToIdle();
        Vector3 targetPos = new Vector3(player.transform.position.x, this.transform.position.y, player.transform.position.z);
        transform.LookAt(targetPos);

        if(readyToAttack)
        {
            switch (currAttack)
            {
                case bossAttack.tongue:
                    tongueAttack();
                    break;

                case bossAttack.iceToMeetYou:
                    iceAttack();
                    break;

                case bossAttack.popmissiles:
                    missileAttack();
                    break;

                default:
                    tongueAttack();
                    break;
            }

            if ((patternProg) + 1 <= (attackPattern.Count - 1))
            {
                patternProg++;
            }
            else
            {
                patternProg = 0;
            }

            currAttack = attackPattern[patternProg];
            readyToAttack = false;
            StartCoroutine(attackDelay());
        }
        

    }

    void tongueAttack()
    {
        spin.spin();
        animator.SetTrigger("tongue");
    }

    void iceAttack()
    {
        animator.SetTrigger("ice");
    }

    void missileAttack()
    {
        animator.SetTrigger("missile");
        for (int i = 0; i < projectilePoints.Count; i++)
        {
            GameObject newMissile = GameObject.Instantiate(choosePopsicle(), projectilePoints[i]);
            newMissile.transform.parent = null;
            newMissile.GetComponent<enemyProjectileHoming>().damage = projectileDamage;
            float missileDelay = i * 0.1f;
            newMissile.GetComponent<enemyProjectileHoming>().beginSequence(missileDelay + 1);
        }

    }


    IEnumerator attackDelay()
    {
        yield return new WaitForSeconds(delayTime);
        readyToAttack = true;
    }

    void goToIdle()
    {
        animator.SetBool("walking", false);
        animator.SetBool("idle", true);
    }

    void goToWalking()
    {
        animator.SetBool("idle", false);
        animator.SetBool("walking", true);
    }

    GameObject choosePopsicle()
    {
        int x = Random.Range(0, projectileTypes.Count);
        return projectileTypes[x];
    }


}
