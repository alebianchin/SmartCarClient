using SmartCarClient.Sensors;
using System;

namespace SmartCarClient
{
    class CarReading
    {
        public RandomGenerator random;
        public TemperatureSensorInterface temp;
        public SpeedSensorInterface speed;
        public GPSSensorInterface gps;
        public DirectionSensorInterface dir;

        public CarReading()
        {
            random = new RandomGenerator();
            temp = new VirtualTemperatureSensor(random);
            speed = new VirtualSpeedSensor(random);
            gps = new VirtualGPSSensor(random);
            dir = new VirtualDirectionSensor();
        }

        public object GetReading()
        {
            RootObject r = new RootObject();
            Field f = new Field();
            f.temperature = temp.getTemperature();
            f.speed = speed.getSpeed();
            f.lat = gps.getLat();
            f.lon = gps.getLon();
            r.fields = new System.Collections.Generic.List<Field>();
            r.fields.Add(f); 
            r.measurement = "misura";
            r.timestamp = (long)(DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
        
            return r;
        }
    }
}
