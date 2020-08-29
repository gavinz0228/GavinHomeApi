using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GavinHomeApi.Utilities;
using GavinHomeApi.YoutubeDownload.Models;
namespace GavinHomeApi.YoutubeDownload.Controllers
{
    [Route("[controller]")]
    public class YoutubeDownloadController : Controller
    {
        public const string DOWNLOAD_TASK_OUTPUT_DIR = "Download";
        private readonly ILogger<YoutubeDownloadController> _logger;
        public YoutubeDownloadController(ILogger<YoutubeDownloadController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("DownloadAsMp3")]
        public async Task<ActionResult> DownloadAsMp3([FromQuery(Name = "url")] string url)
        {
            var filePath = await YoutubeDownloadUtilities.DownloadYoutubeVideoAsMp3(url);
            byte [] buffer = await FileUtilities.ReadAllBytes(filePath);
            System.IO.File.Delete(filePath);
            return File(buffer, "audio/mpeg", System.IO.Path.GetFileName(filePath));
        }
        [HttpPost]
        [Route("CreateTask")]
        public async Task<ActionResult> CreateTask([FromBody] DownloadTask downloadTask)
        {
            var currentDir = PathUtilities.GetCurrentAssemblyDirectory();
            var downloadDir = Path.Combine(currentDir, DOWNLOAD_TASK_OUTPUT_DIR);

            await YoutubeDownloadUtilities.DownloadYoutubeVideoListAsMp3(downloadTask.urls, downloadDir);
            return Ok();
        }
        [HttpPost]
        [Route("CreateTaskForm")]
        public async Task<ActionResult> CreateTaskForm()
        {
            var currentDir = PathUtilities.GetCurrentAssemblyDirectory();
            var downloadDir = Path.Combine(currentDir, DOWNLOAD_TASK_OUTPUT_DIR);

            string [] urls = Request.Form["urls"].ToString().Trim().Split("\n");
            var zipOutputPath = await YoutubeDownloadUtilities.DownloadYoutubeVideoListAsZip(urls);

            byte [] buffer = await FileUtilities.ReadAllBytes(zipOutputPath);
            System.IO.File.Delete(zipOutputPath);
            return File(buffer, "application/octet-stream", System.IO.Path.GetFileName(zipOutputPath));
        }

    }
}
