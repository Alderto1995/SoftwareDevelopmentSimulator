using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody), typeof(Collider),typeof(NavMeshAgent))]
public class CycleController : MonoBehaviour
{
    private Rigidbody rigidbody;
    private NavMeshAgent agent;
    private CycleWaypoint nextPoint;
    private CycleWaypoint previousPoint;
    private Cycle bicycle;
    private float distToGround;

    // Start is called before the first frame update
    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        distToGround = GetComponent<Collider>().bounds.extents.y;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void SetData(CycleWaypoint start, Cycle bicycle)
    {
        nextPoint = start;
        transform.LookAt(new Vector3(nextPoint.Position.x, transform.position.y, nextPoint.Position.z));
        this.bicycle = bicycle;
        agent.acceleration = bicycle.acceleration;
        agent.speed = bicycle.maxSpeed;
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
                    if(nextPoint.Continue == true)
                    {
                        CycleWaypoint wp = nextPoint.GetNextWaypoint(previousPoint);
                        previousPoint = nextPoint;
                        nextPoint = wp;
                        Vector3 wpPos = nextPoint.Position;
                        agent.SetDestination(new Vector3(wpPos.x, transform.position.y, wpPos.z));
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
