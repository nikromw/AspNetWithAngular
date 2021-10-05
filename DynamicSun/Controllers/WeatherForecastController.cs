using DynamicSun.DynamicSun;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DynamicSun.Controllers
{
    public class WeatherController: ControllerBase
    {
        private readonly IDbService _dbService;
        public WeatherController(IDbService dbService)
        {
            _dbService = dbService;
        }

        [HttpGet("GetWeather/{archiveName}/{index}")]
        [ProducesResponseType(typeof(List<Weather>), 200)]
        public IActionResult GetWeather(
            string archiveName,
            int index,
            [FromQuery] int fromYear,
            [FromQuery] int toYear,
            [FromQuery] int fromMonth,
            [FromQuery] int toMonth)
        {
            return Ok(_dbService
                .GetWeatherByFilter(
                archiveName.Split(',').ToList(),
                index, fromYear, toYear, fromMonth, toMonth));
        }
    }
}
