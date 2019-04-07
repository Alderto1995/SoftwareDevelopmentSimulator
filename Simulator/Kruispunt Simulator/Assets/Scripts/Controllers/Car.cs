using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Car", menuName = "CrossSection/Entity/Motor_Vehicle/Car")]
public class Car : ScriptableObject
{
    public GameObject prefab;
    public float maxSpeed;
    public float safeDistance;
}
