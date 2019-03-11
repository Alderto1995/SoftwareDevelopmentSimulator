using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public Vector3 Position { get; private set; }
    public Waypoint WPStraightThrough { get; private set; }
    public Waypoint WPRight { get; private set; }
    public Waypoint WPLeft { get; private set; }

    public GameObject straightThrough;
    public GameObject right;
    public GameObject left;

    // Start is called before the first frame update
    void Start()
    {
        WPStraightThrough = straightThrough.GetComponent<Waypoint>();
        WPRight = right.GetComponent<Waypoint>();
        WPLeft = left.GetComponent<Waypoint>();

        Position = transform.position;
    }
}
