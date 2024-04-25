using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawner : MonoBehaviour
{
    public AudioClip[] clearSound;
    public audioSystem system;

    public GameObject coffeMaker;
    public GameObject airFryer;
    public GameObject iron;
    public GameObject toaster;
    public GameObject printer;
    public GameObject bigChills;
    public enemySpawnPoint[] spawnPoints;
    bool hasSpawned;
    public int enemyRoster;
    public int count;
    public slidingDoor door;
    public bool arena = true;

    AudioSource audioSource;

    void Start()
    {
        system = GameObject.FindGameObjectWithTag("AudioSystem").GetComponent<audioSystem>(); 
        audioSource = gameObject.GetComponent<AudioSource>();
        hasSpawned = false;
        for(int i = 0; i < spawnPoints.Length; i++ )
        {
            enemyRoster++;
        }
    }

    void Update()
    {
        if(count >= enemyRoster && door != null)
        {
            system.returnToDefault();
            print("Destroyed");
            int clip = Random.Range(0,clearSound.Length);
            if(arena)audioSource.PlayOneShot(clearSound[clip]); door.canOpen = true;
            Destroy(gameObject);
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

                    case enemySpawnPoint.enemyType.toaster:
                        spawnEnemy(toaster, spawnPoints[i].transform);
                        break;

                    case enemySpawnPoint.enemyType.printer:
                        spawnEnemy(printer, spawnPoints[i].transform);
                        break;

                    case enemySpawnPoint.enemyType.biggChills:
                        spawnEnemy(bigChills, spawnPoints[i].transform);
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
        enemyToInst.GetComponentInChildren<enemyData>().spawner = gameObject.GetComponent<enemySpawner>();
        //enemyRoster++;
        

    }

    public void addCount()
    {
        print("Counter raised");
        count++;
    }

}
