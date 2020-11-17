﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs
{
    public class CountriesAndCitiesDTO
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public IEnumerable<CityDTO> Cities { get; set; }
    }
}
