using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using uPLibrary.Networking.M2Mqtt.Messages;

[RequireComponent(typeof(Renderer))]
public class TrafficLight : Receiver
{
    public enum State {Red, Yellow, Green, White};

    public int groupId;
    public int componentId;
    public string userType;
    public List<GameObject> stopWaypoints;
    public State StartState;

    private Renderer renderer;
    private List<Waypoint> waypoints;
    private Color color;

    private void Awake()
    {
        topic = $"{userType}/{groupId}/light/{componentId}";
    }

    // Start is called before the first frame update
    void Start()
    {
        Init();
        renderer = GetComponent<Renderer>();
        renderer.material.color = Color.white;
        waypoints = new List<Waypoint>();

        foreach(GameObject t in stopWaypoints)
        {
            Waypoint waypoint = t.GetComponent<Waypoint>();
            waypoint.Continue = false;
            waypoints.Add(waypoint);
        }
        ChangeColor((int)StartState);
    }

    private void Update()
    {
        if(renderer.material.color != color)
        {
            renderer.material.color = color;
        }
    }

    private void ChangeColor(int value)
    {
        switch (value)
        {
            case 0:
                color = Color.red;
                foreach(Waypoint waypoint in waypoints)
                {
                    waypoint.Continue = false;
                }
                break;
            case 1:
                color = Color.yellow;
                foreach (Waypoint waypoint in waypoints)
                {
                    waypoint.Continue = false;
                }
                break;
            case 2:
                color = Color.green;
                foreach (Waypoint waypoint in waypoints)
                {
                    waypoint.Continue = true;
                }
                break;
            case 3:
                color = Color.white;
                foreach (Waypoint waypoint in waypoints)
                {
                    waypoint.Continue = true;
                }
                break;
        }
    }

    //Wordt uitgevoerd wanneer er een bericht binnen is.
    protected override void Client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
    {
        string message = Encoding.UTF8.GetString(e.Message);
        Debug.Log($"Received message from topic: {topic} Message: '{message}'!");
        int.TryParse(message, out int value);
        ChangeColor(value);
    }
}
