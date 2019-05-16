using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Sensor : MonoBehaviour
{
    public int groupId;
    public int componentId;
    public string userType;
    public Transform subSensorTransform;

    private Collider collider;
    private SubSensor subSensor;
    private int counter;

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<Collider>();
        collider.isTrigger = true;

        if(subSensorTransform != null)
        {
            subSensor = subSensorTransform.GetComponent<SubSensor>();
        }
        else
        {
            subSensor = null;
        }

        counter = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(counter <= 0)
        {
            Publisher.instance.SendMessage($"{userType}/{groupId}/sensor/{componentId}", "1");
        }
        counter++;
    }

    private void OnTriggerExit(Collider other)
    {
        if(counter == 1)
        {
            Publisher.instance.SendMessage($"{userType}/{groupId}/sensor/{componentId}", "0");
        }
        counter--;
    }

    public void Entered()
    {
        if(counter <= 0)
        {
            Publisher.instance.SendMessage($"{userType}/{groupId}/sensor/{componentId}", "1");
        }
        counter++;
    }

    public void Left()
    {
        if (counter == 1)
        {
            Publisher.instance.SendMessage($"{userType}/{groupId}/sensor/{componentId}", "0");
        }
        counter--;
    }
}
