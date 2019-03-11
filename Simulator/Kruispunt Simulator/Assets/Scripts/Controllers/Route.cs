using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Route : MonoBehaviour
{
    public Waypoint StartWP { get; private set; }
    public Waypoint DestinationWP { get; private set; }

    public GameObject start;
    public GameObject destination;

    // Start is called before the first frame update
    void Start()
    {
        StartWP = start.GetComponent<Waypoint>();
        DestinationWP = destination.GetComponent<Waypoint>();
    }
}
