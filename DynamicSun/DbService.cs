﻿using DynamicSun.DynamicSun;
using Microsoft.AspNetCore.Http;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DynamicSun
{
    public class DbService : IDbService
    {
        private WeatherContext _weatherContext;
        public DbService(WeatherContext weatherContext)
        {
            _weatherContext = weatherContext;
        }

        public List<string> GetArchives()
        {
            return _weatherContext.Archives.Select(a => a.Name).ToList();
        }

        public List<Weather> GetAllWeather()
        {
            return _weatherContext.Weathers.ToList();
        }


        public void SaveWeatherInDb(IFormFile file , string fileName)
        {
            XSSFWorkbook hssfwb;
            _weatherContext.Archives.Add(new Archive() { Name = fileName });
            hssfwb = new XSSFWorkbook(file.OpenReadStream());
            int sheetNumber = hssfwb.NumberOfSheets;

            for(int i = 0; i < sheetNumber; i++)
            {
                ISheet sheet = hssfwb.GetSheetAt(i);

                for(int row = 5; row <= sheet.LastRowNum; row++)
                {
                    try
                    {
                        if(sheet.GetRow(row) != null)
                        {
                            Weather weather = new Weather();
                            var RowElements = sheet.GetRow(row).Cells;

                            weather.Date = new DateTime(Convert.ToInt32(RowElements[0].ToString().Split('.')[2]),
                                                        Convert.ToInt32(RowElements[0].ToString().Split('.')[1]),
                                                        Convert.ToInt32(RowElements[0].ToString().Split('.')[0]),
                                                        Convert.ToInt32(RowElements[1].ToString().Split(':')[0]),
                                                        Convert.ToInt32(RowElements[1].ToString().Split(':')[1]),
                                                        0);
                            weather.Temp = Convert.ToDouble(RowElements[2].CellType == CellType.String ? RowElements[2].RichStringCellValue.String == " " ? null : RowElements[7].RichStringCellValue.String : RowElements[2].NumericCellValue.ToString());
                            weather.Wet = Convert.ToDouble(RowElements[3].CellType == CellType.String ? RowElements[3].RichStringCellValue.String == " " ? null : RowElements[7].RichStringCellValue.String : RowElements[3].NumericCellValue.ToString());
                            weather.DewPoint = Convert.ToDouble(RowElements[4].CellType == CellType.String ? RowElements[4].RichStringCellValue.String == " " ? null : RowElements[7].RichStringCellValue.String : RowElements[4].NumericCellValue.ToString());
                            weather.Pressure = Convert.ToDouble(RowElements[5].CellType == CellType.String ? RowElements[5].RichStringCellValue.String == " " ? null : RowElements[7].RichStringCellValue.String : RowElements[5].NumericCellValue.ToString());
                            weather.WindDirect = RowElements[6].ToString();
                            weather.WindSpeed = Convert.ToDouble(RowElements[7].CellType == CellType.String ? RowElements[7].RichStringCellValue.String == " " ? null : RowElements[7].RichStringCellValue.String : RowElements[7].NumericCellValue.ToString());
                            weather.CloudCover = Convert.ToDouble(RowElements[8].CellType == CellType.String ? RowElements[8].RichStringCellValue.String == " " ? null : RowElements[7].RichStringCellValue.String : RowElements[8].NumericCellValue.ToString());
                            weather.LowLimitCloud = Convert.ToDouble(RowElements[9].CellType == CellType.String ? RowElements[9].RichStringCellValue.String == " " ? null : RowElements[7].RichStringCellValue.String : RowElements[9].NumericCellValue.ToString());
                            weather.HorizontalVisibility = RowElements[10].ToString();
                            weather.WeatherEffect = RowElements.ElementAtOrDefault(11) == null ? "" : RowElements[11].StringCellValue;
                            weather.ArchiveName = fileName;
                            _weatherContext.Weathers.Add(weather);
                        }
                    }
                    catch(Exception e)
                    {

                    }
                    _weatherContext.SaveChanges();
                }
            }
        }

        public List<Weather> FilterWeather(string yearFrom, string yearTo, string monthFrom, string monthTo)
        {
            return _weatherContext.Weathers.Where(w => ((DateTime)w.Date).Year >= Convert.ToInt32(yearFrom) 
            && ((DateTime)w.Date).Year <= Convert.ToInt32(yearFrom) && ((DateTime)w.Date).Month >= Convert.ToInt32(monthFrom)
            && ((DateTime)w.Date).Month <= Convert.ToInt32(monthTo)).ToList();
        }
    }
}
