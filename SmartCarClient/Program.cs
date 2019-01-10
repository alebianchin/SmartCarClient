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
            RandomGenerator random = new RandomGenerator(); 
            TemperatureSensorInterface temp = new VirtualTemperatureSensor(random);
            SpeedSensorInterface speed = new VirtualSpeedSensor(random);
            GPSSensorInterface gps = new VirtualGPSSensor(random);
            //DirectionSensorInterface dir = new VirtualDirectionSensor();

            t.Start();

            while (true)
            {
                long epoch = (long)(DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
                
                queue.addMessage("{" + string.Format(" \"value\": {0} , \"timestamp\": {1} ", temp.getTemperature(), epoch) + "}","temperature");
                queue.addMessage("{" + string.Format(" \"value\": {0} , \"timestamp\": {1} ", speed.getSpeed(), epoch) + "}", "speed");
                queue.addMessage("{" + string.Format(" \"value\": {0} , \"timestamp\": {1} ", gps.getLat(), epoch) + "}", "GPS/lat");
                queue.addMessage("{" + string.Format(" \"value\": {0} , \"timestamp\": {1} ", gps.getLon(), epoch) + "}", "GPS/lon");
                //queue.addMessage("{" + string.Format(" \"value\": {0} , \"timestamp\": {1} ", dir.getDirection(), epoch) + "}", "dir");


                Console.WriteLine("Added to queue");

                Thread.Sleep(1000);
            }
        }
    }
}



