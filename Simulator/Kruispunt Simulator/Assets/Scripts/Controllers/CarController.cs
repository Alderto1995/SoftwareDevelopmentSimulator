using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class CarController : MonoBehaviour
{
    private Rigidbody rigidbody;
    private Waypoint nextPoint;
    private Car car;
    private float distToGround;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        distToGround = GetComponent<Collider>().bounds.extents.y;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void SetData(Waypoint start, Car car)
    {
        nextPoint = start;
        Waypoint next = GetNewWaypoint(start);
        transform.LookAt(new Vector3(next.Position.x, transform.position.y, next.Position.z));
        this.car = car;
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
    }

    private bool IsCarInFront(Vector3 dir)
    {
        if (Physics.Raycast(transform.position, dir, out RaycastHit hit))
        {
            if(hit.distance <= car.safeDistance && hit.transform.tag == transform.tag)
            {
                return true;
            }
        }
        return false;
    }

    private void Move()
    {
        if (IsGrounded())
        {
            if (nextPoint != null)
            {
                Vector2 nextPosition2D = new Vector2(nextPoint.Position.x, nextPoint.Position.z);
                Vector2 dir = nextPosition2D - new Vector2(transform.position.x, transform.position.z);
                if (dir.magnitude <= 0.1f)
                {
                    if(nextPoint.Continue == true)
                    {
                        nextPoint = GetNewWaypoint(nextPoint);
                    }
                    else
                    {
                        rigidbody.velocity = Vector3.zero;
                    }
                }
                else
                {
                    dir = dir.normalized;
                    Vector3 dir3D = new Vector3(dir.x, 0, dir.y);
                    if (IsCarInFront(dir3D))
                    {
                        rigidbody.velocity = Vector3.zero;
                    }
                    else
                    {
                        rigidbody.velocity = dir3D * car.maxSpeed;
                        transform.LookAt(new Vector3(nextPoint.Position.x, transform.position.y, nextPoint.Position.z));
                    }
                }
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    private Waypoint GetNewWaypoint(Waypoint waypoint)
    {
        List<Transform> waypoints = waypoint.waypoints;
        if(waypoints.Count > 0)
        {
            Transform t = waypoints[Random.Range(0, waypoints.Count)];
            return t.GetComponent<Waypoint>();
        }
        return null;
    }
}
