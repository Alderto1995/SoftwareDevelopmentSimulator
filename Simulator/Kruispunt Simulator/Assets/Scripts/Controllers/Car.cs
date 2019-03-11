using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Car", menuName = "CrossSection/Entity/Vehicles/Car")]
public class Car : ScriptableObject
{
    public GameObject prefab;
    public int maxSpeed;
}
