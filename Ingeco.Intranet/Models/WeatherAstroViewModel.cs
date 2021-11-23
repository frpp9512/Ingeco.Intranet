using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingeco.Intranet.Models
{
    public class WeatherAstroViewModel
    {
        public DateTimeOffset Sunrise { get; set; }
        public DateTimeOffset Sunset { get; set; }
        public DateTimeOffset Moonrise { get; set; }
        public DateTimeOffset Moonset { get; set; }
        public string MoonPhase { get; set; }
        public float MoonIllumination { get; set; }
    }
}