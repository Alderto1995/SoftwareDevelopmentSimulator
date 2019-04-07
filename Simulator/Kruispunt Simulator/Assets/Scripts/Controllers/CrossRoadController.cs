using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossRoadController : MonoBehaviour
{
    public static CrossRoadController instance;
    public Car[] cars;
    public Cycle[] cycles;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnCar(Waypoint start)
    {
        Car car = cars[Random.Range(0,cars.Length)];
        Vector3 pos = start.Position;
        pos.y += 2;
        GameObject go = Instantiate(car.prefab, pos, car.prefab.transform.rotation);
        go.GetComponent<CarController>().SetData(start, car);
    }

    public void SpawnCycle(CycleWaypoint start)
    {
        Cycle cycle = cycles[Random.Range(0, cycles.Length)];
        Vector3 pos = start.Position;
        pos.y += 2;
        GameObject go = Instantiate(cycle.prefab, pos, cycle.prefab.transform.rotation);
        go.GetComponent<CycleController>().SetData(start, cycle);
    }
}
