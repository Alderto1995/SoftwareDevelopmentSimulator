using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentWaypoint : Waypoint
{
    public Transform halfpoint;
    public bool isStart = false;
    //Voor voetgangers, dit moet eigenlijk in een eigen klasse.
    //Doen wanneer hier tijd voor is.
    public bool isFootButton = false;
    public string userType;
    public string groupId;
    public string componentId;

    private bool buttonPushed = false;//Moet weg bij verbetering PedestrianWaypoint.
    private List<AgentWaypoint> agentWaypoints;
    private AgentWaypoint halfwaypoint;

    void Awake()
    {
        Position = transform.position;
        agentWaypoints = new List<AgentWaypoint>();
        Continue = true;
        if (waypoints == null)
        {
            waypoints = new List<Transform>();
        }
        else
        {
            foreach(Transform t in waypoints)
            {
                agentWaypoints.Add(t.GetComponent<AgentWaypoint>());
            }
        }

        if(halfpoint != null)
        {
            halfwaypoint = halfpoint.GetComponent<AgentWaypoint>();
        }
    }

    public AgentWaypoint GetNextWaypoint(AgentWaypoint previous)
    {
        if(previous == null)
        {
            return agentWaypoints[Random.Range(0, agentWaypoints.Count)];
        }
        else if(halfpoint != null && previous != halfwaypoint)
        {
            return halfwaypoint;
        }
        else
        {
            List<AgentWaypoint> points = new List<AgentWaypoint>();
            foreach(AgentWaypoint wp in agentWaypoints)
            {
                if(wp != previous)
                {
                    points.Add(wp);
                }
            }
            return points[Random.Range(0,points.Count)];
        }
    }
    //Moet ook naar een eigen pedestrian waypoint klasse.
    public void PushButton()
    {
        if(groupId != null && componentId != null && userType != null && isFootButton && !buttonPushed)
        {
            buttonPushed = true;
            Publisher.instance.SendMessage($"{userType}/{groupId}/sensor/{componentId}", "1");
        }
    }
    //Moet weg bij verbetering PedestrianWaypoint.
    public void ResetButton()
    {
        if (groupId != null && componentId != null && userType != null && isFootButton && buttonPushed)
        {
            buttonPushed = false;
            Publisher.instance.SendMessage($"{userType}/{groupId}/sensor/{componentId}", "0");
        }
    }
}
