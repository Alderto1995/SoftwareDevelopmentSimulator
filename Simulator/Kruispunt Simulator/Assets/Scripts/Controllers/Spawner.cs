using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Waypoint), typeof(Collider))]
public abstract class Spawner : MonoBehaviour
{
    public float minSpawnTime = 2f;
    public float maxSpawnTime = 6f;

    protected float spawnTime;
    protected Collider colliderSA;
    protected int counter;

    protected void Awake()
    {
        colliderSA = GetComponent<Collider>();
        colliderSA.isTrigger = true;
        counter = 0;
        Init();
    }

    protected virtual void Init()
    {

    }

    protected bool IsFree()
    {
        if (counter > 0) return false;
        else return true;
    }

    protected void OnTriggerEnter(Collider other)
    {
        counter++;
    }

    protected void OnTriggerExit(Collider other)
    {
        counter--;
    }
}
