using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using Assets.Scripts.Controllers;

public class Publisher : Communication
{
    public static Publisher instance;

    //Instantieert de Singleton.
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
            clientId = Guid.NewGuid().ToString();
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
