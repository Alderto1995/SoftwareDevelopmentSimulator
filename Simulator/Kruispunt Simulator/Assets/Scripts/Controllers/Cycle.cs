using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Bicycle", menuName = "CrossSection/Entity/Cycle/Bicycle")]
public class Cycle : ScriptableObject
{
    public GameObject prefab;
    public float maxSpeed;
    public float acceleration;
}
