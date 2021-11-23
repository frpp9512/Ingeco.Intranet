using Ingeco.Intranet.Data.Interfaces;
using Ingeco.Intranet.Data.Models;
using Ingeco.Intranet.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Ingeco.Intranet.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWeatherForecastManager _weather;
        private readonly IPostsManagementRepository _posts;

        public HomeController(ILogger<HomeController> logger, IWeatherForecastManager weather, IPostsManagementRepository posts)
        {
            _logger = logger;
            _weather = weather;
            _posts = posts;
        }

        public async Task<IActionResult> IndexAsync()
        {
            ForecastSummaryViewModel forecastSummary = null;
            try
            {
                var forecast = await _weather.GetForecastAsync();
                var wlocation = forecast.location.GetViewModel();
                var wcurrent = forecast.current.GetViewModel();
                var wforecast = forecast.forecast.GetViewModel();
                forecastSummary = new ForecastSummaryViewModel
                {
                    Forecast = wforecast,
                    CurrentWeather = wcurrent,
                    WeatherLocation = wlocation
                };
            }
            catch (Exception)
            { }
            var posts = await _posts.GetLatestPublicPostsAsync(6);
            var postsvms = posts.Select(p => p.GetViewModel(_posts.GetTotalCommentsCountForPostAsync(p.Id).GetAwaiter().GetResult()));
            var vm = new HomePageViewModel
            {
                ForecastSummary = forecastSummary,
                LatestPosts = postsvms
            };
            return View(vm);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
