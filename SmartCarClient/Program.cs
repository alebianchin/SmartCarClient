using SmartCarClient.Sensors;
using System.Threading;
using System;
using Newtonsoft.Json;

namespace SmartCarClient
{
    class Program
    {
        static void Main(string[] args)
        {
            MessageQueuing queue = new MessageQueuing();
            ThreadStart threadDelegate = new ThreadStart(MQTTSender.sendData);
            Thread t = new Thread(threadDelegate);
            RandomGenerator random;
            TemperatureSensorInterface temp;
            SpeedSensorInterface speed;
            GPSSensorInterface gps;
            //DirectionSensorInterface dir = new VirtualDirectionSensor();

            t.Start();

            while (true)
            {
                random = new RandomGenerator();
                temp = new VirtualTemperatureSensor(random);
                Thread.Sleep(1);
                speed = new VirtualSpeedSensor(random);
                Thread.Sleep(1);
                gps = new VirtualGPSSensor(random);
                long epoch = (long)(DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
                var tempJson = new
                {
                    value = temp.getTemperature(),
                    timestamp = epoch
                };

                var speedJson = new
                {
                    value = speed.getSpeed(),
                    timestamp = epoch
                };
                var latjson = new
                {
                    value = gps.getLat(),
                    timestamp = epoch
                };
                var lonJson = new
                {
                    value = gps.getLon(),
                    timestamp = epoch
                };

                queue.addMessage(JsonConvert.SerializeObject(tempJson),"temperature1");
                queue.addMessage(JsonConvert.SerializeObject(speedJson), "speed1");
                queue.addMessage(JsonConvert.SerializeObject(tempJson), "GPS/lat1");
                queue.addMessage(JsonConvert.SerializeObject(tempJson), "GPS/lon1");

                //queue.addMessage("{" + string.Format(" \"value\": {0} , \"timestamp\": {1} ", dir.getDirection(), epoch) + "}", "dir");


                Console.WriteLine("Added to queue");

                Thread.Sleep(1000);
            }
        }
    }
}



