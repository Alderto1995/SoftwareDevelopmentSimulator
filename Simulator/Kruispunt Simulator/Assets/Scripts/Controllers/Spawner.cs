using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Waypoint))]
public abstract class Spawner : MonoBehaviour
{
    public float minSpawnTime = 2f;
    public float maxSpawnTime = 6f;

    protected float spawnTime;
}
