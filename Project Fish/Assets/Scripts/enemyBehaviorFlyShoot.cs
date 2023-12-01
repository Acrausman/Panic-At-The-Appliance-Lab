using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBehaviorFlyShoot : MonoBehaviour
{
    public List<GameObject> movePoints;
    public float moveSpeed = 10;
    public float delayTime = 1;
    public float detectionDistance = 2;

    public int index = 0;
    Vector3 desiredPoint;
    int previousPoint = 0;

    GameObject target;
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
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, desiredPoint) > 1)
        {
            transform.position = Vector3.MoveTowards(transform.position, desiredPoint, moveSpeed * Time.deltaTime);
        }
        else if (readyToMove)
        {
            moveToNextPoint();
            readyToMove = false;
        }
        else if (!readyToMove) StartCoroutine(attackSequence());
        transform.LookAt(target.transform);
    }



    void moveToNextPoint()
    {
        index = Random.Range(0, movePoints.Count);
        if(index == previousPoint)
        {
            while (index == previousPoint) index = Random.Range(0, movePoints.Count);
        }
        desiredPoint = new Vector3(movePoints[index].transform.position.x, movePoints[index].transform.position.y, movePoints[index].transform.position.z);

        previousPoint = index;
    }

    IEnumerator attackSequence()
    {
        float timeInterval = delayTime;
        animator.SetBool("moving", false);
        animator.SetBool("attacking", true);
        yield return new WaitForSeconds(timeInterval);
        animator.SetBool("attacking", false);
        animator.SetBool("moving", true);
        readyToMove = true;

    }
}
