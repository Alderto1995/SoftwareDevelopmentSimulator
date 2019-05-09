using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VesselSpawner : Spawner
{
    private Waypoint waypoint;

    // Start is called before the first frame update
    protected override void Init()
    {
        waypoint = GetComponent<Waypoint>();
        spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
    }

    // Update is called once per frame
    void Update()
    {
        spawnTime -= Time.deltaTime;
        if (spawnTime <= 0f && IsFree())
        {
            IntersectionController.instance.SpawnVessel(waypoint);
            spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
        }
    }
}
