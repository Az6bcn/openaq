using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.ExternalApiResponseDTO
{
    public class CountriesExternalAPIResponseDTO
    {
        public Meta Meta { get; set; }
        public List<Country> Results { get; set; }
    }

    public class CitiesExternalAPIResponseDTO
    {
        public Meta Meta { get; set; }
        public List<City> Results { get; set; }
    }


    public class MeasurementsExternalAPIResponseDTO
    {
        public Meta Meta { get; set; }
        public List<MeasurementLocation> Results { get; set; }
    }


    public class Meta
    {
        public string Name { get; set; }
        public string Website { get; set; }
        public string License { get; set; }
        public int Page { get; set; }
        public int Limit { get; set; }
        public int Found { get; set; }
    }
}
