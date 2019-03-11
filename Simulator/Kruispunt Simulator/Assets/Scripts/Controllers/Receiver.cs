using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

public class Receiver : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        MqttClient client;
        string broker = "broker.0f.nl";
        string topic = "1";
        string clientId = "test";

        try
        {
            client = new MqttClient(broker);
            client.Connect(clientId);
            client.MqttMsgPublishReceived += client_MqttMsgPublishReceived;
            client.Subscribe(new string[] { topic }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
            Debug.Log("Jooo Verbonden");
        }
        catch (Exception ex)
        {
            Debug.Log(ex);
        }
    }


    static void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
    {
        Debug.Log(Encoding.UTF8.GetString(e.Message));
        // handle message received 
    }

    void Update()
    {

    }
}