using Ingeco.Intranet.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingeco.Intranet.Models
{
    public class WeatherDayViewModel
    {
        public float MaxTemperatureCelsius { get; set; }
        public float MaxTemperatureFahrenheit { get; set; }
        public float MinTemperatureCelsius { get; set; }
        public float MinTemperatureFahrenheit { get; set; }
        public float AverageTempCelsius { get; set; }
        public float AverageTemperatureFahrenheit { get; set; }
        public float MaxWindMph { get; set; }
        public float MaxWindKph { get; set; }
        public float TotalPrecipitationMM { get; set; }
        public float TotalPrecipitationIN { get; set; }
        public float AverageVisibilityKm { get; set; }
        public float AverageVisibilityMiles { get; set; }
        public float AverageHumidity { get; set; }
        public bool WillItRain { get; set; }
        public int RainChance { get; set; }
        public bool WillItSnow { get; set; }
        public int SnowChance { get; set; }
        public WeatherConditionViewModel Condition { get; set; }
        public float UV { get; set; }
    }
}