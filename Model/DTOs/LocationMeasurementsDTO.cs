using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs
{
    public class LocationMeasurementsDTO
    {
        public string SourceName { get; set; }
        public DateTime FirstUpdated { get; set; }
        public DateTime LastUpdated { get; set; }
        public List<string> Parameters { get; set; }
        public Metadata Metadata { get; set; }

        public LocationMeasurementsDTO()
        {
            Parameters = new();
        }
    }
}
