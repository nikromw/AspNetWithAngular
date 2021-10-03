using DynamicSun.DynamicSun;
using Microsoft.AspNetCore.Hosting;
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
    public class UploadFileController
    {
        [HttpPost("UploadFiles")]
        public IActionResult PostFile([FromForm] FileModel fileModel)
        {
            if(fileModel.Content != null)
            {
                DbHelper.SaveWeatherInDb(fileModel.Content, fileModel.Name);
            }
                return null;
        }
    }
}
