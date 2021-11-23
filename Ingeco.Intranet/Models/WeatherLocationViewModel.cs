using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingeco.Intranet.Models
{
    public class WeatherLocationViewModel
    {
        public string Name { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public string TimeZone { get; set; }
        public string LocalTime { get; set; }
    }
}
