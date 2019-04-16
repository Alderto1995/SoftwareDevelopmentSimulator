using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntersectionController : MonoBehaviour
{
    public static IntersectionController instance;
    public Car[] cars;
    public Agent[] cycles;
    public Agent[] pedestrians;

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

    public void SpawnCycle(AgentWaypoint start)
    {
        Agent agent = cycles[Random.Range(0, cycles.Length)];
        Vector3 pos = start.Position;
        pos.y += 2;
        GameObject go = Instantiate(agent.prefab, pos, agent.prefab.transform.rotation);
        go.GetComponent<AgentController>().SetData(start, agent);
    }

    public void SpawnPedestrian(AgentWaypoint start)
    {
        Agent agent = pedestrians[Random.Range(0, pedestrians.Length)];
        Vector3 pos = start.Position;
        pos.y += 2;
        GameObject go = Instantiate(agent.prefab, pos, agent.prefab.transform.rotation);
        go.GetComponent<AgentController>().SetData(start, agent);
    }
}
