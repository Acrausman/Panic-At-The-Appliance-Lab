using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawner : MonoBehaviour
{
    public AudioClip[] clearSound;
    public audioSystem system;

    public GameObject coffeMakerObject;
    public GameObject airFryerObject;
    public GameObject ironObject;
    public GameObject toasterObject;
    public GameObject printerObject;
    public GameObject bigChillsObject;
    public enemySpawnPoint[] spawnPoints;
    bool hasSpawned;
    bool changedMusic;
    public int enemyRoster;
    public int count;
    public slidingDoor door;
    public bool arena = true;
    public int maxWaves;
    int currentWave; //currentWave starts at 0 for ease of enemySpawnPoint setting, so the first wave spawned would be wave 0

    AudioSource audioSource;

    void Start()
    {
        changedMusic = false;
        system = GameObject.FindGameObjectWithTag("AudioSystem").GetComponent<audioSystem>(); 
        audioSource = gameObject.GetComponent<AudioSource>();
        hasSpawned = false;
        currentWave = 0;
        //for(int i = 0; i < spawnPoints.Length; i++ )
        //{
        //    enemyRoster++;
        //}
    }

    void Update()
    {
        if(count >= enemyRoster && door != null)
        {
            system.returnToDefault();
            //print("Destroyed");
            int clip = Random.Range(0,clearSound.Length);
            if (arena)
            {
                if(!changedMusic)audioSource.PlayOneShot(clearSound[clip]); changedMusic = true;
                //check/increment wave, open door if all waves completed
                if (currentWave < maxWaves)
                {
                    count = 0;
                    enemyRoster = 0;
                    hasSpawned = false;
                    waveBegin();
                }
                else {
                    print("All waves cleared! Door opened!");
                    door.canOpen = true;
                    Destroy(gameObject);
                }
            }
            //print("Activated");
            //Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && !hasSpawned)
        {
            waveBegin();
        }
        
    }

    /*void waveBegin() {
        print("Spawning wave: " + (currentWave + 1) + " out of " + maxWaves);
        hasSpawned = true;
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            switch (spawnPoints[i].typeToSpawn)
            {
                case enemySpawnPoint.enemyType.coffeMaker:
                    if (spawnPoints[i].spawnWaves[currentWave]) {
                        spawnEnemy(coffeMaker, spawnPoints[i].transform);
                    }
                    break;

                case enemySpawnPoint.enemyType.airFryer:
                    if (spawnPoints[i].spawnWaves[currentWave])
                    {
                        spawnEnemy(airFryer, spawnPoints[i].transform);
                    }
                    break;

                case enemySpawnPoint.enemyType.iron:
                    if (spawnPoints[i].spawnWaves[currentWave])
                    {
                        spawnEnemy(iron, spawnPoints[i].transform);
                    }
                    break;

                case enemySpawnPoint.enemyType.toaster:
                    if (spawnPoints[i].spawnWaves[currentWave])
                    {
                        spawnEnemy(toaster, spawnPoints[i].transform);
                    }
                    break;

                case enemySpawnPoint.enemyType.printer:
                    if (spawnPoints[i].spawnWaves[currentWave])
                    {
                        spawnEnemy(printer, spawnPoints[i].transform);
                    }
                    break;

                case enemySpawnPoint.enemyType.biggChills:
                    if (spawnPoints[i].spawnWaves[currentWave])
                    {
                        //spawnEnemy(biggChills, spawnPoints[i].transform);
                        //biggChills not recognized?
                    }
                    break;

                default:
                    print("Spawn Point not spawning this wave");
                    break;
            }
        }
        currentWave++;
    }*/

    void waveBegin()
    {
        print("Spawning wave: " + (currentWave + 1) + " out of " + maxWaves);
        hasSpawned = true;
        print("Spawn points length is: " + spawnPoints.Length);
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            print(spawnPoints[i].spawnWaves[currentWave]);
            if (spawnPoints[i]==null) {
                print("spawn point null");
                break;
            }
            switch (spawnPoints[i].spawnWaves[currentWave]) {
                case enemyType.spawnNothing:
                    break;
                case enemyType.coffeMaker:
                    spawnEnemy(coffeMakerObject, spawnPoints[i].transform);
                    break;
                case enemyType.airFryer:
                    spawnEnemy(airFryerObject, spawnPoints[i].transform);
                    break;
                case enemyType.iron:
                    spawnEnemy(ironObject, spawnPoints[i].transform);
                    break;
                case enemyType.toaster:
                    spawnEnemy(toasterObject, spawnPoints[i].transform);
                    break;
                case enemyType.printer:
                    spawnEnemy(printerObject, spawnPoints[i].transform);
                    break;
                case enemyType.biggChills:
                    //spawnEnemy(biggChillsObject, spawnPoints[i].transform);
                    //spawnEnemy(biggChills, spawnPoints[i].transform);
                    //biggChills not recognized?
                    break;
                default:
                    print("Spawn Point broken, tried to spawn but failed");
                    break;
            }
        }
        currentWave++;
    }

    void spawnEnemy(GameObject type, Transform point)
    {
        Vector3 desiredPos = new Vector3(point.position.x, point.position.y, point.position.z);
        GameObject enemyToInst = Instantiate(type, desiredPos, Quaternion.identity);
        enemyToInst.GetComponentInChildren<enemyData>().spawner = gameObject.GetComponent<enemySpawner>();
        enemyRoster++;
    }

    public void addCount()
    {
        print("Enemies killed: " + count + " out of " + enemyRoster);
        count++;
    }

}
