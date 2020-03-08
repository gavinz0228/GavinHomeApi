using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GavinHomeApi.Utilities;

namespace GavinHomeApi.Controllers
{
    [Route("")]
    public class HomeController : Controller
    {
        [HttpGet]
        public ViewResult Index()
        {
            string baseUrl = Request.Scheme + "://" + Request.Host;
            ViewBag.Usage = $"Download a single Youtube Video as a mp3 file {baseUrl}/YoutubeDownload/DownloadAsMp3?url=<Youtbe URL>";
            
            return View();
        }
    }
}
