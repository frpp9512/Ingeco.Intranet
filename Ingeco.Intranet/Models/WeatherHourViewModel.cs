using Ingeco.Intranet.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingeco.Intranet.Models
{
    public class WeatherHourViewModel
    {
        public DateTimeOffset Time { get; set; }
        public float TemperatureCelsius { get; set; }
        public float TemperatureFahrenheit { get; set; }
        public int DayNumber { get; set; }
        public WeatherConditionViewModel Condition { get; set; }
        public float WindMph { get; set; }
        public float WindKph { get; set; }
        public int WindDegree { get; set; }
        public string WindDirection { get; set; }
        public float PressureMb { get; set; }
        public float PressureIn { get; set; }
        public float PrecipitationMM { get; set; }
        public float PrecipitationIN { get; set; }
        public int Humidity { get; set; }
        public int Cloud { get; set; }
        public float TemperatureSensationCelsius { get; set; }
        public float TemperatureSensationFahrenheit { get; set; }
        public float HeatIndexCelsius { get; set; }
        public float HeatIndexFahrenheit { get; set; }
        public float DewPointCelsius { get; set; }
        public float DewPointFahrenheit { get; set; }
        public bool WillItRain { get; set; }
        public int RainChance { get; set; }
        public bool WillItSnow { get; set; }
        public int SnowChance { get; set; }
        public float VisibilityKm { get; set; }
        public float VisibilityMiles { get; set; }
        public float GustMph { get; set; }
        public float GustKph { get; set; }
        public float UV { get; set; }
    }
}
