using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawner : MonoBehaviour
{
    public GameObject coffeMaker;
    public GameObject airFryer;
    public GameObject iron;
    public enemySpawnPoint[] spawnPoints;
    bool hasSpawned;
    public List<GameObject> enemyRoster;
    [HideInInspector]public int count;
    public slidingDoor door;

    void Start()
    {
        hasSpawned = false;
        count = 0;
    }

    void Update()
    {
        if(count == enemyRoster.Count + 1)
        {
            door.canOpen = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && !hasSpawned)
        {
            hasSpawned = true;
            for (int i = 0; i < spawnPoints.Length; i++)
            {
                switch (spawnPoints[i].typeToSpawn)
                {
                    case enemySpawnPoint.enemyType.coffeMaker:
                        spawnEnemy(coffeMaker, spawnPoints[i].transform);
                        break;

                    case enemySpawnPoint.enemyType.airFryer:
                        spawnEnemy(airFryer, spawnPoints[i].transform);
                        break;

                    case enemySpawnPoint.enemyType.iron:
                        spawnEnemy(iron, spawnPoints[i].transform);
                        break;

                    default:
                        spawnEnemy(coffeMaker, spawnPoints[i].transform);
                        break;
                }

            }
        }
        
    }

    void spawnEnemy(GameObject type, Transform point)
    {
        Vector3 desiredPos = new Vector3(point.position.x, point.position.y, point.position.z);
        GameObject enemyToInst = Instantiate(type, desiredPos, Quaternion.identity);
        enemyRoster.Add(enemyToInst);

    }

}
