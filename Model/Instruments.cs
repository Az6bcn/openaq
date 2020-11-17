using System.Collections.Generic;

namespace Model
{
    public class Instruments
    {
        public string Type { get; set; }
        public string Active { get; set; }
        public List<string> Parameters { get; set; }
        public string SerialNumber { get; set; }

        public Instruments()
        {
            Parameters = new();
        }
    }
}