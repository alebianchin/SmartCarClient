using System;

using System.Messaging;

using System.Text;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace SmartCarClient
{
    class MQTTSender
    {
        public static MessageQueuing messageQueuing;
        public static void sendData()

        {
            messageQueuing = new MessageQueuing();
            MqttClient client = new MqttClient(@"test.mosquitto.org");
            string clientId = Guid.NewGuid().ToString();
            client.Connect(clientId);
            while (true) {
                try
                {
                    if (client.IsConnected)
                    {
                        Console.WriteLine(client.IsConnected);
                    }
                        messageQueuing.messageQueue.MessageReadPropertyFilter.Priority = true;
                        Message m = messageQueuing.messageQueue.Receive();
                    
                        m.Formatter = new XmlMessageFormatter(new string[] { "System.String,mscorlib" });
                        string topic = "kitt/cars/car1/" + m.Label;
                    
                        Console.WriteLine("sending data to {0} ", topic );
                        //client.Publish(topic, Encoding.UTF8.GetBytes(m.Body.ToString()), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, true);
                        client.Publish(topic, Encoding.UTF8.GetBytes(m.Body.ToString()), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, true);
                        Console.WriteLine(m.Body.ToString());
                }
                catch (Exception e)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("ERROR {0}", e.StackTrace);
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
            } 
            }
        }
    }

