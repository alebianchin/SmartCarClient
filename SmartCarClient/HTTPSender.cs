using System;
using System.IO;
using System.Messaging;
using System.Net;

namespace SmartCarClient
{
    class HTTPSender
    {
        public static MessageQueuing messageQueuing;
        public static void sendData()
        {
            messageQueuing = new MessageQueuing();
            while (true)
            { 
                Message m = messageQueuing.messageQueue.Receive();
                m.Formatter = new XmlMessageFormatter(new string[] { "System.String,mscorlib" });
                try
                {
                    //HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("http://localhost:8080/data");
                    HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("http://192.168.101.121:8080/cars/car1/data");
                    httpWebRequest.ContentType = "text/json";
                    httpWebRequest.Method = "POST";
                    httpWebRequest.Timeout = 5000;

                    using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                    {
                        streamWriter.Write(m.Body);
                    }
                    var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                    Console.Out.WriteLine("Sent : {0}", m.Body.ToString());
                    httpResponse.Close();
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
