using Ingeco.Intranet.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingeco.Intranet.Models
{
    public class WeatherForecastDayViewModel
    {
        public DateTimeOffset Date { get; set; }
        public WeatherDayViewModel Day { get; set; }
        public WeatherAstroViewModel Astro { get; set; }
        public WeatherHourViewModel[] Hourly { get; set; }
    }
}