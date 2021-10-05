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
        List<string> SaveWeatherInDb(IFormFile file, string fileName);
        List<Weather> GetWeatherByFilter(List<string> archive,int index, int yearFrom, int yearTo, int monthFrom, int monthTo);
    }
}
