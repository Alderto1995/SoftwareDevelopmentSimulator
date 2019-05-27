using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntersectionController : MonoBehaviour
{
    public static IntersectionController instance;
    public Car[] cars;
    public Car bus;
    public Agent[] cycles;
    public Agent[] pedestrians;
    public Vessel[] vessels;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        Publisher.instance.SendMessage("features/lifecycle/simulator/onconnect","");
    }

    public void SpawnCar(Waypoint start)
    {
        Car car = cars[Random.Range(0,cars.Length)];
        Vector3 pos = start.Position;
        pos.y += 2;
        GameObject go = Instantiate(car.prefab, pos, car.prefab.transform.rotation);
        go.GetComponent<CarController>().SetData(start, car);
    }

    public void SpawnBus(Waypoint start)
    {
        Vector3 pos = start.Position;
        pos.y += 2;
        GameObject go = Instantiate(bus.prefab, pos, bus.prefab.transform.rotation);
        go.GetComponent<CarController>().SetData(start, bus);
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

    public void SpawnVessel(Waypoint start)
    {
        Vessel vessel = vessels[Random.Range(0, vessels.Length)];
        Vector3 pos = start.Position;
        pos.y += 2;
        GameObject go = Instantiate(vessel.prefab, pos, vessel.prefab.transform.rotation);
        go.GetComponent<VesselController>().SetData(start, vessel);
    }
}
