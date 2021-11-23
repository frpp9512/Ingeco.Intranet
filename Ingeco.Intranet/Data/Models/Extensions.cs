using Ingeco.Intranet.Models;
using SmartB1t.Security.WebSecurity.Local;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingeco.Intranet.Data.Models
{
    public static class Extensions
    {
        public static User GetModel(this CreateUserViewModel viewModel)
            => new()
            {
                Fullname = viewModel.Fullname,
                Department = viewModel.Department,
                Position = viewModel.Position,
                Email = viewModel.Email,
                Active = true
            };

        public static EditUserViewModel GetEditViewModel(this User user)
            => new() 
            {
                Id = user.Id.ToString(),
                Fullname = user.Fullname,
                Email = user.Email,
                Department = user.Department,
                Position = user.Position,
                RolesSelected = user.Roles.Select(ur => ur.Role.Id.ToString()).ToArray(),
                ProfilePictureId = user.Id.ToString()
            };


        public static CategoryViewModel GetViewModel(this Category category)
            => category is null ? throw new ArgumentNullException("The category cannot be null") : new()
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description
            };

        public static Category GetModel(this CategoryViewModel viewModel)
            => new()
            {
                Id = viewModel.Id,
                Name = viewModel.Name,
                Description = viewModel.Description
            };

        public static PostViewModel GetViewModel(this Post model, int commentsCount = 0)
            => new()
            {
                Id = model.Id,
                Title = model.Title,
                Body = model.Body,
                Description = model.Description,
                Category = model.Category?.GetViewModel(),
                Created = model.Created,
                Media = model.Media?.Select(m => m.GetViewModel()),
                PostedBy = model.PostedBy,
                Public = model.Public,
                TagsLine = model.TagsLine,
                Comments = model.Comments?.Select(c => c.GetViewModel()),
                TotalCommentsCount = commentsCount
            };

        public static WebMediaViewModel GetViewModel(this WebMedia model)
            => new() 
            {
                Id = model.Id,
                Filename = model.Filename,
                Description = model.Description,
                IsCover = model.IsCover,
                MediaType = model.MediaType,
                PostId = model.PostId
            };

        public static CommentViewModel GetViewModel(this Comment model)
            => model is null ? null : new()
            {
                Id = model.Id,
                Created = model.Created,
                IsReply = model.IsReply,
                Post = model.Post,
                PostId = model.PostId,
                RepliedTo = model.RepliedTo?.GetViewModel(),
                RepliedToId = model.RepliedToId,
                Replies = model.Replies?.Select(r => r.GetViewModel() ?? new()),
                Text = model.Text,
                User = model.User,
                UserId = model.UserId
            };

        public static Comment GetModel(this CommentViewModel viewModel)
            => viewModel is null ? null : new()
            {
                Id = viewModel.Id,
                Created = viewModel.Created,
                IsReply = viewModel.IsReply,
                Post = viewModel.Post,
                PostId = viewModel.PostId,
                RepliedTo = viewModel.RepliedTo?.GetModel(),
                RepliedToId = viewModel.RepliedToId,
                Replies = viewModel.Replies?.Select(r => r.GetModel() ?? new()),
                Text = viewModel.Text,
                User = viewModel.User,
                UserId = viewModel.UserId
            };

        public static WeatherLocationViewModel GetViewModel(this Location model)
            => new()
            {
                Country = model.country,
                Latitude = model.lat,
                LocalTime = model.localtime,
                Longitude = model.lon,
                Name = model.name,
                Region = model.region,
                TimeZone = model.tz_id
            };

        public static WeatherConditionViewModel GetViewModel(this Condition model)
            => new()
            {
                Code = model.code,
                IconUrl = model.icon,
                Text = model.text
            };

        public static WeatherCurrentViewModel GetViewModel(this Current model)
            => new()
            {
                Cloud = model.cloud,
                Condition = model.condition.GetViewModel(),
                DayNumber = model.is_day,
                GustKph = model.gust_kph,
                GustMph = model.gust_mph,
                Humidity = model.humidity,
                PrecipitationInches = model.precip_in,
                PrecipitationMM = model.precip_mm,
                PressureIn = model.pressure_in,
                PressureMb = model.pressure_mb,
                TemperatureCelsius = model.temp_c,
                TemperatureFahrenheit = model.temp_f,
                TemperatureSensationCelsius = model.feelslike_c,
                TemperatureSensationFahrenheit = model.feelslike_f,
                Updated = DateTimeOffset.Parse(model.last_updated),
                UvIndex = model.uv,
                VisibilityKm = model.vis_km,
                VisibilityMiles = model.vis_miles,
                WindDegree = model.wind_degree,
                WindDirection = model.wind_dir,
                WindKph = model.wind_kph,
                WindMph = model.wind_mph
            };

        public static WeatherForecastViewModel GetViewModel(this Forecast model)
            => new()
            {
                Daily = model.forecastday.Select(f => f.GetViewModel()).ToArray()
            };

        public static WeatherForecastDayViewModel GetViewModel(this Forecastday model)
            => new()
            {
                Date = DateTimeOffset.Parse(model.date),
                Astro = model.astro.GetViewModel(),
                Day = model.day.GetViewModel(),
                Hourly = model.hour.Select(h => h.GetViewModel()).ToArray()
            };

        public static WeatherAstroViewModel GetViewModel(this Astro model)
            => new()
            {
                MoonIllumination = float.Parse(model.moon_illumination),
                Moonrise = DateTimeOffset.Parse(model.moonrise),
                Moonset = DateTimeOffset.Parse(model.moonset),
                Sunrise = DateTimeOffset.Parse(model.sunrise),
                Sunset = DateTimeOffset.Parse(model.sunset),
                MoonPhase = model.moon_phase
            };

        public static WeatherDayViewModel GetViewModel(this Day model)
            => new()
            {
                AverageHumidity = model.avghumidity,
                AverageTempCelsius = model.avgtemp_c,
                AverageTemperatureFahrenheit = model.avgtemp_f,
                AverageVisibilityKm = model.avgvis_km,
                AverageVisibilityMiles = model.avgvis_miles,
                Condition = model.condition.GetViewModel(),
                MaxTemperatureCelsius = model.maxtemp_c,
                MaxTemperatureFahrenheit = model.maxtemp_f,
                MaxWindKph = model.maxwind_kph,
                MaxWindMph = model.maxwind_mph,
                MinTemperatureCelsius = model.mintemp_c,
                MinTemperatureFahrenheit = model.mintemp_f,
                RainChance = model.daily_chance_of_rain,
                SnowChance = model.daily_chance_of_snow,
                TotalPrecipitationIN = model.totalprecip_in,
                TotalPrecipitationMM = model.totalprecip_mm,
                UV = model.uv,
                WillItRain = model.daily_will_it_rain == 1,
                WillItSnow = model.daily_will_it_snow == 1
            };

        public static WeatherHourViewModel GetViewModel(this Hour model)
            => new()
            {
                Cloud = model.cloud,
                Condition = model.condition.GetViewModel(),
                DayNumber = model.is_day,
                DewPointCelsius = model.dewpoint_c,
                DewPointFahrenheit = model.dewpoint_f,
                GustKph = model.gust_kph,
                GustMph = model.gust_mph,
                HeatIndexCelsius = model.heatindex_c,
                HeatIndexFahrenheit = model.heatindex_f,
                Humidity = model.humidity,
                PrecipitationIN = model.precip_in,
                PrecipitationMM = model.precip_mm,
                PressureIn = model.pressure_in,
                PressureMb = model.pressure_mb,
                RainChance = model.chance_of_rain,
                SnowChance = model.chance_of_snow,
                TemperatureCelsius = model.temp_c,
                TemperatureFahrenheit = model.temp_f,
                TemperatureSensationCelsius = model.feelslike_c,
                TemperatureSensationFahrenheit = model.feelslike_f,
                Time = DateTimeOffset.Parse(model.time),
                UV = model.uv,
                VisibilityKm = model.vis_km,
                VisibilityMiles = model.vis_miles,
                WillItRain = model.will_it_rain == 1,
                WillItSnow = model.will_it_snow == 1,
                WindDegree = model.wind_degree,
                WindDirection = model.wind_dir,
                WindKph = model.wind_kph,
                WindMph = model.wind_mph
            };
    }
}