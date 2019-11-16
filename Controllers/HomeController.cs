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
    [Route("")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public ActionResult Index()
        {
            string usage = "Download Youtube Video as a mp3 file http://bygavin.com:8000/YoutubeDownload/DownloadAsMp3?url=<Youtbe URL>";
            return Ok(usage);
        }
    }
}
