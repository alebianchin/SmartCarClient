using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCarClient
{
    public class Field
    {
        public decimal temperature { get; set; }
        public decimal speed { get; set; }
        public decimal lat { get; set; }
        public decimal lon { get; set; }
    }

    public class RootObject
    {
        public string measurement { get; set; }
        public List<Field> fields { get; set; }
        public long timestamp { get; set; }
    }
}
