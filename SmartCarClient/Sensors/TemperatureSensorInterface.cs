using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartCarClient.Sensors
{
    interface TemperatureSensorInterface
    {
        void setTemperature(decimal temperature);
        decimal getTemperature();
        string toJson();
    }
}
