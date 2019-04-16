using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody),typeof(NavMeshAgent))]
public class AgentController : MonoBehaviour
{
    private Rigidbody rigidbody;
    private NavMeshAgent navAgent;
    private AgentWaypoint nextPoint;
    private AgentWaypoint previousPoint;
    private Agent agent;

    private bool pushedButton = false;//Moet weg bij verbetering PedestrianWaypoint.

    // Start is called before the first frame update
    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        navAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void SetData(AgentWaypoint start, Agent agent)
    {
        nextPoint = start;
        transform.LookAt(new Vector3(nextPoint.Position.x, transform.position.y, nextPoint.Position.z));
        this.agent = agent;
        navAgent.acceleration = agent.acceleration;
        navAgent.speed = agent.maxSpeed;
    }

    private void Move()
    {
        if (nextPoint != null)
        {
            Vector2 nextPosition2D = new Vector2(nextPoint.Position.x, nextPoint.Position.z);
            Vector2 dir = nextPosition2D - new Vector2(transform.position.x, transform.position.z);
            if (dir.magnitude <= 0.5f)
            {
                if(nextPoint.isStart == true && previousPoint != null)
                {
                    Destroy(gameObject);
                }
                else
                {
                    //Moet in eigen PedestrianController wanneer hier tijd voor is.
                    if (nextPoint.isFootButton == true && pushedButton == false)
                    {
                        nextPoint.PushButton();
                        pushedButton = true;
                    }

                    if (nextPoint.Continue == true)
                    {
                        nextPoint.ResetButton();//Moet weg bij verbetering PedestrianWaypoint.
                        AgentWaypoint wp = nextPoint.GetNextWaypoint(previousPoint);
                        previousPoint = nextPoint;
                        nextPoint = wp;
                        Vector3 wpPos = nextPoint.Position;
                        navAgent.SetDestination(new Vector3(wpPos.x, transform.position.y, wpPos.z));
                        pushedButton = false;//Moet weg bij verbetering PedestrianWaypoint.
                    }
                }
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
