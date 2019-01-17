using System;
using System.Messaging;
using CoAP;

namespace SmartCarClient
{
    class COAPSender
    {
        public static MessageQueuing messageQueuing;
        public static void sendData()
        {
            messageQueuing = new MessageQueuing();
            while (true)
            {
                System.Messaging.Message m = messageQueuing.messageQueue.Receive();
                m.Formatter = new XmlMessageFormatter(new string[] { "System.String,mscorlib" });
                try
                {
                    //HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("http://localhost:8080/data");
                    //CoapClient client = new CoapClient();
                    //client.Uri = new Uri("coap://192.168.101.99:5683/sensor");

                    Request request = new Request(Method.POST);
                    request.URI = new Uri("coap://192.168.101.99:5683/sensor");
                    request.SetPayload(m.Body.ToString(), MediaType.ApplicationJson);
                    
                    request.Send();
                    var response = request.WaitForResponse();

                    if (response == null)
                    {
                        // timeout
                        Console.WriteLine("Waiting for an internet connection to send data...");
                        messageQueuing.messageQueue.Send(m.Body.ToString());
                    }
                    else
                    {
                        // success
                        Console.Out.WriteLine("Sent : {0}", m.Body.ToString());
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine("Waiting for an internet connection to send data...");
                    messageQueuing.messageQueue.Send(m.Body.ToString());
                }

            }
        }
    }
}
