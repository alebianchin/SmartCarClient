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
            var tempJson = temp.getTemperature();
            var speedJson = speed.getSpeed();
            var latJson = gps.getLat();
            var lonJson = gps.getLon();
            long epoch = (long)(DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;

            var x = new
            {
                fields = new[] {
                        new { fldName = "temperatura", value = tempJson },
                        new { fldName = "velocità", value = speedJson },
                        new { fldName = "lat", value = latJson },
                        new { fldName = "lon", value =lonJson }
                    },
                tags = new[] {
                        new { fldName = "temperatura", value = tempJson },
                        new { fldName = "velocità", value =speedJson },
                        new { fldName = "lat", value = latJson },
                        new { fldName = "lon", value = lonJson }
                    },
                direction = dir.getDirection(),
                timestamp = epoch
            };
            return x;
        }
    }
}
