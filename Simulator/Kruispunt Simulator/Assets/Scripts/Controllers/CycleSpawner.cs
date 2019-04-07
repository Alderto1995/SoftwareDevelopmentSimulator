using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CycleSpawner : Spawner
{
    private CycleWaypoint waypoint;

    // Start is called before the first frame update
    void Awake()
    {
        waypoint = GetComponent<CycleWaypoint>();
        spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
    }

    // Update is called once per frame
    void Update()
    {
        spawnTime -= Time.deltaTime;
        if (spawnTime <= 0f)
        {
            CrossRoadController.instance.SpawnCycle(waypoint);
            spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
        }
    }
}
