using Ingeco.Intranet.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingeco.Intranet.Models
{
    public class WeatherCurrentViewModel
    {
        public DateTimeOffset Updated { get; set; }
        public float TemperatureCelsius { get; set; }
        public float TemperatureFahrenheit { get; set; }
        public int DayNumber { get; set; }
        public WeatherConditionViewModel Condition { get; set; }
        public float WindMph { get; set; }
        public float WindKph { get; set; }
        public int WindDegree { get; set; }
        public string WindDirection { get; set; }
        
        /// <summary>
        /// Pressure in Millibar
        /// </summary>
        public float PressureMb { get; set; }
        public float PressureIn { get; set; }
        public float PrecipitationMM { get; set; }
        public float PrecipitationInches { get; set; }
        public int Humidity { get; set; }
        public int Cloud { get; set; }
        public float TemperatureSensationCelsius { get; set; }
        public float TemperatureSensationFahrenheit { get; set; }
        public float VisibilityKm { get; set; }
        public float VisibilityMiles { get; set; }
        public float UvIndex { get; set; }
        public float GustMph { get; set; }
        public float GustKph { get; set; }
    }
}