using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using uPLibrary.Networking.M2Mqtt.Messages;

[RequireComponent(typeof(Renderer))]
public class TrafficLight : MonoBehaviour
{
    public int groupId;
    public int componentId;
    public GameObject stopWaypoint;

    private Renderer renderer;
    private Receiver receiver;
    private Waypoint waypoint;

    private void Awake()
    {
        receiver = new Receiver($"motor_vehicle/{groupId}/light/{componentId}");
        receiver.Client.MqttMsgPublishReceived += client_MqttMsgPublishReceived;
    }

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
        renderer.material.color = Color.red;
        waypoint = stopWaypoint.GetComponent<Waypoint>();
        waypoint.Continue = false;
    }

    private void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
    {
        string message = Encoding.UTF8.GetString(e.Message);
        int.TryParse(message, out int value);

        switch (value)
        {
            case 0:
                renderer.material.color = Color.red;
                waypoint.Continue = false;
                break;
            case 1:
                renderer.material.color = Color.yellow;
                waypoint.Continue = false;
                break;
            case 2:
                renderer.material.color = Color.green;
                waypoint.Continue = true;
                break;
            case 3:
                renderer.material.color = Color.white;
                waypoint.Continue = true;
                break;
        }
    }
}
