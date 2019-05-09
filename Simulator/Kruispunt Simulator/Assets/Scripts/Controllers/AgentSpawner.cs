using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentSpawner : Spawner
{
    public enum AgentType { Pedestrian, Bicycle };
    public AgentType entityType;

    private AgentWaypoint waypoint;

    // Start is called before the first frame update
    protected override void Init()
    {
        waypoint = GetComponent<AgentWaypoint>();
        spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
    }

    // Update is called once per frame
    void Update()
    {
        spawnTime -= Time.deltaTime;
        if (spawnTime <= 0f)
        {
            SpawnAgent();
            spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
        }
    }

    private void SpawnAgent()
    {
        if(entityType == AgentType.Bicycle)
        {
            IntersectionController.instance.SpawnCycle(waypoint);
        }
        else if(entityType == AgentType.Pedestrian)
        {
            IntersectionController.instance.SpawnPedestrian(waypoint);
        }
    }
}
