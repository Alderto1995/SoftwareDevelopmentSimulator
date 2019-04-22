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
    private float halfSizeZ;
    private int layerMask;

    // Start is called before the first frame update
    void Start()
    {
        halfSizeZ = (transform.lossyScale.z * 0.5f);
        rigidbody = GetComponent<Rigidbody>();
        distToGround = GetComponent<Collider>().bounds.extents.y;
        layerMask = 1 << 11;//car layer
    }

    // Update is called once per frame
    void Update()
    {
        //Move();
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
        if (Physics.Raycast(transform.position, dir, out RaycastHit hit, Mathf.Infinity, layerMask))
        {
            if ((hit.distance - halfSizeZ) <= car.safeDistance && hit.transform.tag == transform.tag)
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

                Vector2 dirNor = dir.normalized;
                Vector3 dir3D = new Vector3(dirNor.x, 0, dirNor.y);
                if (IsCarInFront(dir3D))
                {
                    rigidbody.velocity = Vector3.zero;
                }
                else if(dir.magnitude > 1f)
                {
                    rigidbody.velocity = dir3D * car.maxSpeed;
                    transform.LookAt(new Vector3(nextPoint.Position.x, transform.position.y, nextPoint.Position.z));
                }
                else if(dir.magnitude <= 1f)
                {
                    if (nextPoint.Continue == true)
                    {
                        nextPoint = GetNewWaypoint(nextPoint);
                    }
                    else
                    {
                        rigidbody.velocity = Vector3.zero;
                    }
                }
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    private void FixedUpdate()
    {
        Move();
    }
    //bij waypoint rework moet dit in de waypoint gebeuren.
    private Waypoint GetNewWaypoint(Waypoint waypoint)
    {
        List<Waypoint> waypoints = new List<Waypoint>();
        foreach(Transform t in waypoint.waypoints)
        {
            Waypoint wp = t.GetComponent<Waypoint>();
            if(wp.isBus && car.isBus)
            {
                return wp;
            }
            else if(!wp.isBus)
            {
                waypoints.Add(wp);
            }
        }

        if(waypoints.Count > 0)
        {
            return waypoints[Random.Range(0, waypoints.Count)];
        }
        return null;
    }
}
