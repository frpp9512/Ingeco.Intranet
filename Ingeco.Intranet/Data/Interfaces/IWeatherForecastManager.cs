using Ingeco.Intranet.Data.Models;
using System.Threading.Tasks;

namespace Ingeco.Intranet.Data.Interfaces
{
    public interface IWeatherForecastManager
    {
        Task<WeatherForecastResponse> GetForecastAsync();
    }
}