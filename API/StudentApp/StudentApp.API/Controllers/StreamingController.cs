using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using System.IO;
using System.Threading.Tasks;

namespace StudentApp.Controllers
{
    [ApiController]
    [Route("streaming")]
    public class StreamingController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;

        public StreamingController(IWebHostEnvironment env)
        {
            _env = env;
        }

        [HttpGet("{fileName}")]
        public IActionResult StreamVideo(string fileName)
        {
            var filePath = Path.Combine(_env.WebRootPath, "Videos", fileName);

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }

            var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, true);
            var contentType = GetContentType(filePath);

            return new FileStreamResult(fileStream, contentType)
            {
                EnableRangeProcessing = true
            };
        }

        private string GetContentType(string path)
        {
            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(path, out var contentType))
            {
                contentType = "application/octet-stream";
            }
            return contentType;
        }
    }
}
