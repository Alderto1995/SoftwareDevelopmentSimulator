using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Car", menuName = "Intersection/Entity/Motor_Vehicle")]
public class Car : Entity
{
    public float safeDistance;
    public bool isBus;//Dit moet anders bij rework waypoints.
}
