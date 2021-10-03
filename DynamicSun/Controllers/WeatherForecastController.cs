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

        [HttpGet("GetAllWeather")]
        [ProducesResponseType(typeof(List<Weather>), 200)]
        public IActionResult GetWeather()
        {
            return Ok(_dbService.GetAllWeather());
        }
    }
}
