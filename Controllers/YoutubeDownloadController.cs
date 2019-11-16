using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GavinHomeApi.Utilities;

namespace GavinHomeApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class YoutubeDownloadController : ControllerBase
    {
        private readonly ILogger<YoutubeDownloadController> _logger;
        public YoutubeDownloadController(ILogger<YoutubeDownloadController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("DownloadAsMp3")]
        public async Task<ActionResult> DownloadAsMp3([FromQuery(Name = "url")] string url)
        {
            
            var filePath = await YoutubeUtilities.DownloadYoutubeVideoAsMp3(url);
            byte [] buffer = await FileUtilities.ReadAllBytes(filePath);
            System.IO.File.Delete(filePath);
            return File(buffer, "audio/mpeg");
        }
    }
}
