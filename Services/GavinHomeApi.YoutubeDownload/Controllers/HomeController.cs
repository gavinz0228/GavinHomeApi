using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GavinHomeApi.Utilities;

<<<<<<< HEAD:Services/GavinHomeApi.YoutubeDownload/Controllers/HomeController.cs
namespace GavinHomeApi.Controllers
=======
namespace GavinHomeApi.YoutubeDownload.Controllers
>>>>>>> 3322475e28eb3b32a2161b4b9b549057a4f605e6:Services/GavinHomeApi.YoutubeDownload/Controllers/HomeController.cs
{
    [Route("")]
    public class HomeController : Controller
    {
        [HttpGet]
        public ViewResult Index()
        {
<<<<<<< HEAD:Services/GavinHomeApi.YoutubeDownload/Controllers/HomeController.cs
=======
            string baseUrl = Request.Scheme + "://" + Request.Host;
            ViewBag.Usage = $"Download a single Youtube Video as a mp3 file {baseUrl}/YoutubeDownload/DownloadAsMp3?url=<Youtbe URL>";
            
>>>>>>> 3322475e28eb3b32a2161b4b9b549057a4f605e6:Services/GavinHomeApi.YoutubeDownload/Controllers/HomeController.cs
            return View();
        }
    }
}
