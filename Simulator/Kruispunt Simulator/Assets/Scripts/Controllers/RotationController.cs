using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using uPLibrary.Networking.M2Mqtt.Messages;

public class RotationController : Receiver
{
    public enum RotationAxis { X, Y, Z }

    public int groupId;
    public int componentId;
    public string userType;
    public string componentType;
    public float degreesPerSecond = 9f;
    public int maxRotation = 90;
    public int minRotation = 0;
    public RotationAxis axis;
    public bool isDeck = false;

    private bool open;

    void Awake()
    {
        topic = $"{userType}/{groupId}/{componentType}/{componentId}";
    }

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        if (open)
        {
            Rotate(maxRotation);
        }
        else
        {
            Rotate(minRotation);
        }
    }

    private void Rotate(float angle)
    {
        if (axis == RotationAxis.X)
        {
            transform.localRotation = Quaternion.RotateTowards(transform.localRotation, Quaternion.Euler(angle, transform.localRotation.y, transform.localRotation.z), degreesPerSecond * Time.deltaTime);
        }
        else if (axis == RotationAxis.Y)
        {
            transform.localRotation = Quaternion.RotateTowards(transform.localRotation, Quaternion.Euler(transform.localRotation.x, angle, transform.localRotation.z), degreesPerSecond * Time.deltaTime);
        }
        if (axis == RotationAxis.Z)
        {
            transform.localRotation = Quaternion.RotateTowards(transform.localRotation, Quaternion.Euler(transform.localRotation.x, transform.localRotation.y, angle), degreesPerSecond * Time.deltaTime);
        }
    }

    protected override void Client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
    {
        string message = Encoding.UTF8.GetString(e.Message);
        Debug.Log($"Received message from topic: {topic} Message: '{message}'!");
        int.TryParse(message, out int value);

        switch (value)
        {
            case 0:
                if (isDeck)
                {
                    open = true;
                }
                else
                {
                    open = false;
                }
                break;
            case 1:
                if (isDeck)
                {
                    open = false;
                }
                else
                {
                    open = true;
                }
                break;
        }
    }
}
