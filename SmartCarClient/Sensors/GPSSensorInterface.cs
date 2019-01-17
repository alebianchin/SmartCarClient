﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartCarClient.Sensors
{
    interface GPSSensorInterface
    {
        
        void setLat(decimal Lat);
        void setLon(decimal Lon);
        decimal getLat();
        decimal getLon();
        string toJson();
    }
}

