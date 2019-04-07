using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CycleWaypoint : Waypoint
{
    public Transform halfpoint;
    public bool isStart = false;

    private List<CycleWaypoint> cycleWaypoints;
    private CycleWaypoint halfwaypoint;

    // Start is called before the first frame update
    void Awake()
    {
        Position = transform.position;
        cycleWaypoints = new List<CycleWaypoint>();
        Continue = true;
        if (waypoints == null)
        {
            waypoints = new List<Transform>();
        }
        else
        {
            foreach(Transform t in waypoints)
            {
                cycleWaypoints.Add(t.GetComponent<CycleWaypoint>());
            }
        }

        if(halfpoint != null)
        {
            halfwaypoint = halfpoint.GetComponent<CycleWaypoint>();
        }
    }

    public CycleWaypoint GetNextWaypoint(CycleWaypoint previous)
    {
        if(previous == null)
        {
            return cycleWaypoints[Random.Range(0, cycleWaypoints.Count)];
        }
        else if(halfpoint != null && previous != halfwaypoint)
        {
            return halfpoint.GetComponent<CycleWaypoint>();
        }
        else
        {
            List<CycleWaypoint> points = new List<CycleWaypoint>();
            foreach(CycleWaypoint wp in cycleWaypoints)
            {
                if(wp != previous)
                {
                    points.Add(wp);
                }
            }
            return points[Random.Range(0,points.Count)];
        }
    }
}
