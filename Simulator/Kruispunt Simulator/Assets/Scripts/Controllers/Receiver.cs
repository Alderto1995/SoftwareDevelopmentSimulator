using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

public class Receiver : MonoBehaviour
{
    public MqttClient Client { get; private set; }

    private string broker = "broker.0f.nl";
    private string clientId = "team1";
    private int teamId = 1;

    public Receiver(string topic)
    {
        try
        {
            topic = $"{teamId}/{topic}";
            Client = new MqttClient(broker);
            Client.Connect(clientId);
            Client.Subscribe(new string[] { topic }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
            Debug.Log($"Receiver: Connected to topic: '{topic}'!");
        }
        catch (Exception ex)
        {
            Debug.Log(ex);
        }
    }

    static void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
    {
        Debug.Log(Encoding.UTF8.GetString(e.Message));
    }
}