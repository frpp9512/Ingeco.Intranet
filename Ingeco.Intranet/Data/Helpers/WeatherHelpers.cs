using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingeco.Intranet.Data.Helpers
{
    public static class WeatherHelpers
    {
        private static Dictionary<string, string> _weatherConditions = new Dictionary<string, string> 
        {
            { "Cloudy", "Nublado" },
            { "Partly cloudy", "Parcialmente nublado" },
            { "Rainy", "Lluvioso" },
            { "Fog", "Niebla" },
            { "Sunny", "Soleado" }
        };

        private static Dictionary<Range, string> _uvIndexRangeAlerts = new Dictionary<Range, string>
        {
            { 1..2, "Un valor de 2 o menos significa el menor peligro por los rayos Ultravioletas (UV) del sol para una persona promedio." },
            { 3..5, "Un valor de 3 a 5 significa un riesgo MODERADO de daño por exposición desprotegida al sol. Tomar precauciones y evitar salir al mediodía." },
            { 6..7, "Un valor de 6 a 7 significa un riesgo ALTO de daño por exposición desprotegida al sol. Tomar precauciones y evitar salir de 10 AM a 4 PM." },
            { 8..10, "Un valor de 8 a 10 significa un riesgo MUY ALTO de daño por exposición desprotegida al sol. Tomar precauciones, evitar salir de 10 AM a 4 PM, evitar estar en playas u otras zonas con alta reflexión de rayos UV." },
            { 11.., "Un valor mayor que 11 significa un riesgo EXTREMO de daño por exposición desprotegida al sol. Tomar precauciones, evitar salir de 10 AM a 4 PM, evitar estar en playas u otras zonas con alta reflexión de rayos UV, usar bloqueador solar de factor no menor a 30 cada 2 horas." }
        };

        private static Dictionary<string, string> _windDirections = new Dictionary<string, string>
        {
            { "N", "Norte" },
            { "NE", "Noreste" },
            { "E", "Este" },
            { "SE", "Sureste" },
            { "ESE", "Este-sureste" },
            { "S", "Sur" },
            { "SW", "Suroeste" },
            { "WSW", "Oseste-suroeste" },
            { "W", "Oeste" },
            { "WNW", "Oeste-noroeste" },
            { "NW", "Noroeste" }
        };

        public static string GetWeatherConditionInSpanish(string condition)
            => _weatherConditions.ContainsKey(condition) ? _weatherConditions[condition] : condition;

        public static string GetUvAlert(float uvIndex)
        {
            foreach (var rangeAlert in _uvIndexRangeAlerts)
            {
                if (uvIndex >= rangeAlert.Key.Start.Value && uvIndex <= rangeAlert.Key.End.Value)
                {
                    return rangeAlert.Value;
                }
            }
            return "Índice de rayos UV";
        }

        public static string GetWindDirectionText(string windDirection)
            => _windDirections.ContainsKey(windDirection) ? _windDirections[windDirection] : windDirection;
    }
}
