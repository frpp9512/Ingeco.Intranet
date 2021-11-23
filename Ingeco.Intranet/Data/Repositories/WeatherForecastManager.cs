using Ingeco.Intranet.Data.Interfaces;
using Ingeco.Intranet.Data.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Ingeco.Intranet.Data.Repositories
{
    public class WeatherForecastManager : IWeatherForecastManager
    {
        private readonly HttpClient _httpClient;

        public WeatherForecastManager(IOptions<WeatherApiConfig> config)
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri($"http://api.weatherapi.com/v1/forecast.json?key={config.Value.ApiKey}&q={config.Value.Location}&days={config.Value.ForecastDaysAmount}&aqi=no&alerts=no"),
            };
        }

        public async Task<WeatherForecastResponse> GetForecastAsync()
        {
            var response = await _httpClient.GetAsync("");
            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                var weatherForecastResponse = JsonConvert.DeserializeObject<WeatherForecastResponse>(responseData);
                return weatherForecastResponse;
            }
            throw new Exception($"Error while fetching forecast data. Status code: {response.StatusCode}, error message: {response.ReasonPhrase}");
        }
    }
}