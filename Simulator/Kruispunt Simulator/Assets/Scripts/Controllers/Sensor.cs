using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Sensor : MonoBehaviour
{
    public int groupId;
    public int componentId;
    public string userType;

    private Collider collider;
    private int carCounter;

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<Collider>();
        collider.isTrigger = true;
        carCounter = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(carCounter <= 0)
        {
            Publisher.instance.SendMessage($"{userType}/{groupId}/sensor/{componentId}", "1");
        }
        carCounter++;
    }

    private void OnTriggerExit(Collider other)
    {
        if(carCounter == 1)
        {
            Publisher.instance.SendMessage($"{userType}/{groupId}/sensor/{componentId}", "0");
        }
        carCounter--;
    }
}
