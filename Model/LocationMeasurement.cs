using System;
using System.Collections.Generic;

namespace Model
{
    public class LocationMeasurement
    {
        public int Count { get; set; }
        public string SourceName { get; set; }
        public DateTime FirstUpdated { get; set; }
        public DateTime LastUpdated { get; set; }
        public List<string> Parameters { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Location { get; set; }
        public Metadata Metadata { get; set; }

        public LocationMeasurement()
        {
            Parameters = new();
        }
    }
}
