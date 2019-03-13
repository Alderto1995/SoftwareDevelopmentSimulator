using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

public class Publisher : MonoBehaviour
{
    public static Publisher instance;

    private MqttClient client;
    private string broker = "broker.0f.nl";
    private string clientId = "team1";
    private int teamId = 1;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            Init();
        }
    }

    private void Init()
    {
        try
        {
            client = new MqttClient(broker);
            client.Connect(clientId);
            Debug.Log($"Publisher: connected to broker: '{broker}'!");
        }
        catch (Exception ex)
        {
            Debug.Log(ex);
        }
    }
    
    public void SendMessage(string topic, string message)
    {
        topic = $"{teamId}/{topic}";
        client.Publish(topic, Encoding.UTF8.GetBytes(message), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, false);
        Debug.Log($"Message: '{message}' sent to topic: '{topic}'!");
    }
}
