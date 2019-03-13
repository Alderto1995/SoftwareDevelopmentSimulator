using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Route : MonoBehaviour
{
    public List<GameObject> waypoints;

    public List<Waypoint> Waypoints { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        Waypoints = new List<Waypoint>();

        foreach(GameObject gameObject in waypoints)
        {
            Waypoints.Add(gameObject.GetComponent<Waypoint>());
        }
    }
}
