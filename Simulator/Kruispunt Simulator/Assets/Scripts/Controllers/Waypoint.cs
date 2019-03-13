using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public Vector3 Position { get; private set; }
    public bool Continue { get; set; }

    // Start is called before the first frame update
    void Awake()
    {
        Position = transform.position;
        Continue = true;
    }
}
