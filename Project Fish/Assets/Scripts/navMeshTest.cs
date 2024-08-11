using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class navMeshTest : MonoBehaviour
{
    NavMeshAgent agent;
    GameObject player;
    public float dist = 10;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) >= dist)
        {
            agent.isStopped = false;
            agent.SetDestination(player.transform.position);
        }
        else agent.isStopped = true;
    }
}
