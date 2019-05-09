using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : Spawner
{
    public bool canSpawnBus = false;

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
            if(canSpawnBus && Random.Range(0, 10) == 1)
            {
                IntersectionController.instance.SpawnBus(waypoint);
            }
            else
            {
                IntersectionController.instance.SpawnCar(waypoint);
            }
            spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
        }
    }
}
