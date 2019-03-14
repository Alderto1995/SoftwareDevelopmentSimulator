using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using uPLibrary.Networking.M2Mqtt.Messages;

[RequireComponent(typeof(Renderer))]
public class TrafficLight : Receiver
{
    public int groupId;
    public int componentId;
    public GameObject stopWaypoint;

    private Renderer renderer;
    private Receiver receiver;
    private Waypoint waypoint;
    private Color color;

    private void Awake()
    {
        topic = $"motor_vehicle/{groupId}/light/{componentId}";
    }

    // Start is called before the first frame update
    void Start()
    {
        Init();
        renderer = GetComponent<Renderer>();
        renderer.material.color = Color.red;
        color = Color.red;
        waypoint = stopWaypoint.GetComponent<Waypoint>();
        waypoint.Continue = false;
    }

    private void Update()
    {
        if(renderer.material.color != color)
        {
            renderer.material.color = color;
        }
    }

    protected override void Client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
    {
        string message = Encoding.UTF8.GetString(e.Message);
        int.TryParse(message, out int value);

        switch (value)
        {
            case 0:
                color = Color.red;
                waypoint.Continue = false;
                break;
            case 1:
                color = Color.yellow;
                waypoint.Continue = false;
                break;
            case 2:
                color = Color.green;
                waypoint.Continue = true;
                break;
            case 3:
                color = Color.white;
                waypoint.Continue = true;
                break;
        }
    }
}
