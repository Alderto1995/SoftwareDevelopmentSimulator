using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

public class Receiver : MonoBehaviour
{
    protected MqttClient client;
    protected string broker = "broker.0f.nl";
    protected int teamId = 1;
    protected string topic = "";

    protected void Init(string clientId)
    {
        try
        {
            topic = $"{teamId}/{topic}";
            client = new MqttClient(broker);
            client.Connect(clientId);
            client.MqttMsgPublishReceived += Client_MqttMsgPublishReceived;
            client.Subscribe(new string[] { topic }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE } );
            Debug.Log($"Receiver: Connected to topic: '{topic}'!");
        }
        catch (Exception ex)
        {
            Debug.Log(ex);
        }
    }

    protected virtual void Client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
    {
        string message = Encoding.UTF8.GetString(e.Message);
        Debug.Log($"Received message: '{message}'!");
    }
}