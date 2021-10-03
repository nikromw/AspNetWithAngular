using DynamicSun.DynamicSun;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DynamicSun
{
    public interface IDbService
    {
        List<string> GetArchives();
        void SaveWeatherInDb(IFormFile file, string fileName);
         List<Weather> GetAllWeather();
    }
}
