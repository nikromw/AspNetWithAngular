using Microsoft.AspNetCore.Http;

namespace DynamicSun
{
    public class FileModel
    {
        public string Name { get; set; }
        public IFormFile Content { get; set; }
    }
}
