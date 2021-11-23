using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingeco.Intranet.Models
{
    public class ForecastSummaryViewModel
    {
        public WeatherLocationViewModel WeatherLocation { get; set; }
        public WeatherCurrentViewModel CurrentWeather { get; set; }
        public WeatherForecastViewModel Forecast { get; set; }
    }
}