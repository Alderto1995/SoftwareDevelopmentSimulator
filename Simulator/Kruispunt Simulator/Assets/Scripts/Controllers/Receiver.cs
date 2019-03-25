using Assets.Scripts.Controllers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

public abstract class Receiver : Communication
{
    protected string topic = "";

    //Initialiseert de receiver door verbinding te maken met de broker.
    protected void Init()
    {
        try
        {
            clientId = Guid.NewGuid().ToString();
            topic = $"{teamId}/{topic}";
            client = new MqttClient(broker);
            client.Connect(clientId);
            client.MqttMsgPublishReceived += Client_MqttMsgPublishReceived;
            client.Subscribe(new string[] { topic }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE } );
            Debug.Log($"Receiver: Connected to topic: '{topic}'!");
        }
        catch (Exception ex)
        {
            Debug.Log(ex);
        }
    }

    //Deze methode is een eventhandler, deze wordt uitgevoerd wanneer er een bericht binnen is.
    protected virtual void Client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
    {
        string message = Encoding.UTF8.GetString(e.Message);
        Debug.Log($"Received message: '{message}'!");
    }
}