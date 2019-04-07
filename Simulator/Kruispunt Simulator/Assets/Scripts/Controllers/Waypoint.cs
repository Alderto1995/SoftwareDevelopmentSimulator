using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public Vector3 Position { get; protected set; }
    public bool Continue { get; set; }
    public List<Transform> waypoints;

    // Start is called before the first frame update
    void Awake()
    {
        Position = transform.position;
        Continue = true;
        if(waypoints == null)
        {
            waypoints = new List<Transform>();
        }
    }
}
