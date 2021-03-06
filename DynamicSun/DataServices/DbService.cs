using DynamicSun.DynamicSun;
using Microsoft.AspNetCore.Http;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DynamicSun
{
    public class DbService: IDbService
    {
        private WeatherContext _weatherContext;

        public DbService(WeatherContext weatherContext)
        {
            _weatherContext = weatherContext;
        }

        public List<string> GetArchives() => _weatherContext.Archives.Select(a => a.Name).ToList();

        public List<string> SaveWeatherInDb(IFormFile file, string archiveName)
        {
            List<string> returnVal = new List<string>();

            try
            {
                XSSFWorkbook filestreamRead = new XSSFWorkbook(file.OpenReadStream());

                if (_weatherContext.Archives.Any(a => a.Name == archiveName))
                {
                    returnVal.Add($"Такой архив уже был загружен {archiveName}");
                }
                else
                {
                    _weatherContext.Archives.Add(new Archive() { Name = archiveName });

                    returnVal.Add($"Был загружен {archiveName}");

                    for (int i = 0; i < filestreamRead.NumberOfSheets; i++)
                    {
                        ISheet sheet = filestreamRead.GetSheetAt(i);

                        for (int row = 5; row <= sheet.LastRowNum; row++)
                        {
                            try
                            {
                                if (sheet.GetRow(row) != null)
                                {
                                    Weather weather = new Weather();
                                    var RowElements = sheet.GetRow(row).Cells;

                                    weather.Date = new DateTime(
                                        Convert.ToInt32(RowElements[0].ToString().Split('.')[2]),
                                        Convert.ToInt32(RowElements[0].ToString().Split('.')[1]),
                                        Convert.ToInt32(RowElements[0].ToString().Split('.')[0]),
                                        Convert.ToInt32(RowElements[1].ToString().Split(':')[0]),
                                        Convert.ToInt32(RowElements[1].ToString().Split(':')[1]),
                                        0);

                                    weather.Temp = Convert.ToDouble(RowElements[2].CellType == CellType.String
                                        ? RowElements[2].RichStringCellValue.String == " "
                                            ? null
                                            : RowElements[7].RichStringCellValue.String
                                        : RowElements[2].NumericCellValue.ToString());

                                    weather.Wet = Convert.ToDouble(RowElements[3].CellType == CellType.String
                                        ? RowElements[3].RichStringCellValue.String == " "
                                            ? null
                                            : RowElements[7].RichStringCellValue.String
                                        : RowElements[3].NumericCellValue.ToString());

                                    weather.DewPoint = Convert.ToDouble(RowElements[4].CellType == CellType.String
                                        ? RowElements[4].RichStringCellValue.String == " "
                                            ? null
                                            : RowElements[7].RichStringCellValue.String
                                        : RowElements[4].NumericCellValue.ToString());

                                    weather.Pressure = Convert.ToDouble(RowElements[5].CellType == CellType.String
                                        ? RowElements[5].RichStringCellValue.String == " "
                                            ? null
                                            : RowElements[7].RichStringCellValue.String
                                        : RowElements[5].NumericCellValue.ToString());

                                    weather.WindDirect = RowElements[6].ToString();

                                    weather.WindSpeed = Convert.ToDouble(RowElements[7].CellType == CellType.String
                                        ? RowElements[7].RichStringCellValue.String == " "
                                            ? null
                                            : RowElements[7].RichStringCellValue.String
                                        : RowElements[7].NumericCellValue.ToString());

                                    weather.CloudCover = Convert.ToDouble(RowElements[8].CellType == CellType.String
                                        ? RowElements[8].RichStringCellValue.String == " "
                                            ? null
                                            : RowElements[7].RichStringCellValue.String
                                        : RowElements[8].NumericCellValue.ToString());

                                    weather.LowLimitCloud = Convert.ToDouble(RowElements[9].CellType == CellType.String
                                        ? RowElements[9].RichStringCellValue.String == " "
                                            ? null
                                            : RowElements[7].RichStringCellValue.String
                                        : RowElements[9].NumericCellValue.ToString());

                                    weather.HorizontalVisibility = RowElements[10].ToString();

                                    weather.WeatherEffect = RowElements.ElementAtOrDefault(11) == null
                                        ? ""
                                        : RowElements[11].StringCellValue;


                                    Weather existingWeather = _weatherContext.Weathers
                                        .Where(w => w.Date == weather.Date).FirstOrDefault();

                                    if (existingWeather != null)
                                        existingWeather = weather;

                                    weather.ArchiveName = archiveName;

                                    _weatherContext.Weathers.Add(weather);
                                }
                            }
                            catch (Exception ex)
                            {
                                returnVal.Add($"При чтении возникла ошибка файл: {archiveName} страница:{i} строка:{row}.");
                            }

                            _weatherContext.SaveChanges();
                        }
                    }
                }
            }
            catch(Exception)
            {
                returnVal.Add($"Невозможно прочитать файл {archiveName}.");
            }

            return returnVal;
        }

        public List<Weather> GetWeatherByFilter(List<string> archive, int index, int yearFrom, int yearTo, int monthFrom, int monthTo)
        {
            var weathers = _weatherContext
                .Weathers.OrderBy(w => w.Date)
                .Where(w => archive.Contains(w.ArchiveName)
                    && ((DateTime)w.Date).Year >= yearFrom
                    && ((DateTime)w.Date).Year <= yearTo
                    && ((DateTime)w.Date).Month >= monthFrom
                    && ((DateTime)w.Date).Month <= monthTo);

            if (index * 10 < weathers.Count())
            {
                return weathers.Skip(index * 10).Take(10).ToList();
            }

            return null;
        }
    }
}
