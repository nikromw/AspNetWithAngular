using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DynamicSun
{
    public class FileModel
    {
        public string Name { get; set; }
        public IFormFile Content { get; set; }
    }
}
