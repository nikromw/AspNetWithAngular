using DynamicSun.DynamicSun;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicSun.Controllers
{
    public class UploadFileController : Controller
    {
        private readonly IDbService _dbService;
        public UploadFileController(IDbService dbService)
        {
            _dbService = dbService;
        }

        [HttpPost("UploadFiles")]
        [ProducesResponseType(typeof(List<string>), 200)]
        public IActionResult PostFile([FromForm] FileModel fileModel)
        {
            if(fileModel.Content != null)
            {
                return Ok(_dbService.SaveWeatherInDb(fileModel.Content, fileModel.Content.FileName));
            }

            return Ok("Files loaded.");
        }

        [HttpGet("GetArchives")]
        [ProducesResponseType(typeof(List<string>), 200)]
        public IActionResult GetArchives() => Ok(_dbService.GetArchives());
    }
}
