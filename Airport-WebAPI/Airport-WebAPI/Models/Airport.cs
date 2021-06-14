using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Airport_WebAPI.Models
{
    public class Airport
    {
        public string IATACode { get; set; }
        public string City { get; set; }

        public string Name { get; set; }

        public float Longitude {get; set;}
        public float Latitude { get; set; }

    }
}
