using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.InteropServices;
namespace GavinHomeApi.Utilities
{
    public class YoutubeUtilities
    {
        const string WORKING_DIR = "Working";
        const string YOUTUBE_DL_FILE_NAME = "youtube-dl";
        public static async Task<string> DownloadYoutubeVideoAsMp3(string urlToDownload)
        {
            var currentDir = PathUtilities.GetCurrentAssemblyDirectory();
            var workingDir = Path.Combine(currentDir, WORKING_DIR);
            var outputPath = Path.Combine(workingDir, Guid.NewGuid().ToString() + ".mp3");
            if(!Directory.Exists(workingDir))
                Directory.CreateDirectory(workingDir);

            return await DownloadYoutubeVideoAsMp3(urlToDownload, outputPath);
        }

        public async static Task<string> DownloadYoutubeVideoAsMp3(string youtubeUrl, string outputPath)
        {
            string downloaderPath = YOUTUBE_DL_FILE_NAME;
            if(RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                downloaderPath = Path.Combine(PathUtilities.GetCurrentAssemblyDirectory(), YOUTUBE_DL_FILE_NAME);

            var args = $"-x --audio-format mp3 {youtubeUrl} -o {outputPath}";
            //Console.WriteLine(args)
            await Task.Run(()=>{
                var process  = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = downloaderPath,
                        Arguments = args
                    }
                };
                process.Start();
                process.WaitForExit();
            });

            if(File.Exists(outputPath))
                return outputPath;
            else
                throw new Exception("download faied, please check the log");

        }
    }
}