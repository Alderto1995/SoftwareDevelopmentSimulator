using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class VesselController : MonoBehaviour
{
    private Rigidbody rigidbody;
    private Waypoint nextPoint;
    private Vessel vessel;
    private float distToGround;
    private float halfSizeZ;
    private int layerMask;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        distToGround = GetComponent<Collider>().bounds.extents.y;
        layerMask = 1 << 12;//vessel layer
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void SetData(Waypoint start, Vessel vessel)
    {
        nextPoint = start;
        Waypoint next = GetNewWaypoint(start);
        transform.LookAt(new Vector3(next.Position.x, transform.position.y, next.Position.z));
        this.vessel = vessel;
        halfSizeZ = (vessel.width * 0.5f);
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
    }

    private bool IsVesselInFront(Vector3 dir)
    {
        if (Physics.Raycast(transform.position, dir, out RaycastHit hit, Mathf.Infinity, layerMask))
        {
            if ((hit.distance - halfSizeZ) <= vessel.safeDistance && hit.transform.tag == transform.tag)
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
                if (IsVesselInFront(dir3D))
                {
                    rigidbody.velocity = Vector3.zero;
                }
                else if (dir.magnitude > 1f)
                {
                    rigidbody.velocity = dir3D * vessel.maxSpeed;
                    transform.LookAt(new Vector3(nextPoint.Position.x, transform.position.y, nextPoint.Position.z));
                }
                else if (dir.magnitude <= 1f)
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

    //bij waypoint rework moet dit in de waypoint gebeuren.
    private Waypoint GetNewWaypoint(Waypoint waypoint)
    {
        if(waypoint.waypoints.Count > 0)
        {
            return waypoint.waypoints[Random.Range(0, waypoint.waypoints.Count)].GetComponent<Waypoint>();
        }
        return null;
    }
}
