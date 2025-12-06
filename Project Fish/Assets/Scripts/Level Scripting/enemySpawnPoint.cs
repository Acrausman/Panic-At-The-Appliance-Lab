using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum enemyType
{
    spawnNothing,
    coffeMaker,
    airFryer,
    iron,
    toaster,
    printer,
    biggChills
}

public class enemySpawnPoint : MonoBehaviour
{
    public enemyType[] spawnWaves;

    //public enemyType typeToSpawn;

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
