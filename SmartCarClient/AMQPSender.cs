using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Messaging;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace SmartCarClient
{
    class AMQPSender
    {
        public static MessageQueuing messageQueuing;
        public static void SendData()
        {
            messageQueuing = new MessageQueuing();
            var factory = new ConnectionFactory();
             factory.HostName = "bee.rmq.cloudamqp.com";
            factory.Protocol = Protocols.AMQP_0_9_1;
            factory.UserName = "put your username";
            factory.VirtualHost = "put your vhost";
            factory.Password = "put your password";

            factory.Port = 5672;
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                while (true)
                {
                   
                    channel.ExchangeDeclare(exchange: "kitt/cars", type: "topic");
                    Message m = messageQueuing.messageQueue.Receive();

                    m.Formatter = new XmlMessageFormatter(new string[] { "System.String,mscorlib" });
                    var routingKey = "car1";

                    var body = Encoding.UTF8.GetBytes(m.Body.ToString());
                    channel.BasicPublish(exchange: "kitt/cars", routingKey: routingKey, basicProperties: null, body: body);
                    Console.WriteLine(" [x] Sent '{0}':'{1}'", routingKey, m.Body.ToString());
                }
            }
        }
    }
}
