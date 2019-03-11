using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

public class Publisher : MonoBehaviour
{
    MqttClient client;
    // Start is called before the first frame update
    void Start()
    {
        string broker = "broker.0f.nl";
        string topic = "1";
        string clientId = "test";
        string message = "Hello from C#!";

        try
        {
            client = new MqttClient(broker);
            client.Connect(clientId);
            client.Publish(topic, Encoding.UTF8.GetBytes(message), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, false);
            Debug.Log($"Message sent: {message}");
        }
        catch (Exception ex)
        {
            Debug.Log(ex);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
