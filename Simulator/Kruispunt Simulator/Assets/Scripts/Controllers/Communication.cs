using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace Assets.Scripts.Controllers
{
    public abstract class Communication : MonoBehaviour
    {
        protected int teamId = 1;
        protected string clientId;
        protected string broker = "broker.0f.nl";
        protected MqttClient client;
    }
}
