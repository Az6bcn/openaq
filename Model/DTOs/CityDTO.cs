﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs
{
    public class CityDTO
    {
        public string Name { get; set; }
        public IEnumerable<LocationDTO> Locations { get; set; }
    }
}
