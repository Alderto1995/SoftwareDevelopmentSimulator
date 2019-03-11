using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CarController : MonoBehaviour
{
    private Rigidbody rigidbody;
    private Waypoint destination;
    private Waypoint currWaypoint;
    private Car car;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = transform.position - currWaypoint.Position;
        if (dir.magnitude <= 0.5f)
        {
            if (currWaypoint.Equals(destination))
            {
                Destroy(this);
            }
            else
            {
                //pathfinding links en rechts
                currWaypoint = currWaypoint.WPStraightThrough;
            }
        }
        rigidbody.velocity = dir.normalized * car.maxSpeed;
    }

    public void SetData(Waypoint currWaypoint, Waypoint destination, Car car)
    {
        this.currWaypoint = currWaypoint;
        this.destination = destination;
        this.car = car;
    }
}
