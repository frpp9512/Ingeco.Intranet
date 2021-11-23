using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingeco.Intranet.Data.Models
{
    public class WeatherApiConfig
    {
        public string ApiKey { get; set; }
        public string Location { get; set; }
        public int ForecastDaysAmount { get; set; }
    }
}
