using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubSensor : MonoBehaviour
{
    public Transform parentSensorTransform;

    private Collider collider;
    private Sensor parentSensor;

    void Start()
    {
        collider = GetComponent<Collider>();
        collider.isTrigger = true;

        parentSensor = parentSensorTransform.GetComponent<Sensor>();
    }

    private void OnTriggerEnter(Collider other)
    {
        parentSensor.Entered();
    }

    private void OnTriggerExit(Collider other)
    {
        parentSensor.Left();
    }
}
