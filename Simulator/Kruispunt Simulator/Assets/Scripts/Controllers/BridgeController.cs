using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using uPLibrary.Networking.M2Mqtt.Messages;

public class BridgeController : Receiver
{
    public int groupId;
    public int componentId;
    public string userType;
    public int degreesPerSecond = 9;
    public int maxRotation = 90;
    public int minRotation = 0;

    private bool openBridge;

    // Start is called before the first frame update
    void Awake()
    {
        topic = $"{userType}/{groupId}/light/{componentId}";
    }

    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        RotateBridge();
    }

    private void RotateBridge()
    {
        float rotZ = transform.eulerAngles.z;
        if (rotZ > 359)
        {
            transform.localRotation = Quaternion.Euler(transform.eulerAngles.z, transform.eulerAngles.y, minRotation);
            rotZ = minRotation;
        }
        else if (rotZ > maxRotation)
        {
            transform.localRotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, maxRotation);
            rotZ = maxRotation;
        }

        if(rotZ < maxRotation && openBridge)
        {
            transform.Rotate(new Vector3(0, 0, degreesPerSecond) * Time.deltaTime, Space.Self);
        }
        else if(rotZ > minRotation && !openBridge)
        {
            transform.Rotate(new Vector3(0, 0, -degreesPerSecond) * Time.deltaTime, Space.Self);
        }
    }

    protected override void Client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
    {
        string message = Encoding.UTF8.GetString(e.Message);
        Debug.Log($"Received: {message}");
        int.TryParse(message, out int value);

        switch (value)
        {
            case 0:
                openBridge = false;
                break;
            case 1:
                openBridge = true;
                break;
        }
    }
}
