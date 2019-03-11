using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossRoadController : MonoBehaviour
{
    public GameObject routesGO;
    public Car[] cars;

    private List<Route> routes = new List<Route>();
    private float spawnTimer;
    private float maxSpawnTime = 10f;

    // Start is called before the first frame update
    void Start()
    {
        foreach(Transform child in routesGO.transform)
        {
            routes.Add(child.GetComponent<Route>());
        }
        spawnTimer = maxSpawnTime;
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer -= Time.deltaTime;
        if(spawnTimer <= 0)
        {
            spawnTimer = maxSpawnTime;
            SpawnCar();
        }
    }

    private void SpawnCar()
    {
        Route route = routes[Random.Range(0, routes.Count)];
        Car car = cars[Random.Range(0,cars.Length)];
        GameObject go = Instantiate(car.prefab);
        go.transform.position = route.StartWP.Position;
        go.GetComponent<CarController>().SetData(route.StartWP, route.DestinationWP, car);
    }
}
