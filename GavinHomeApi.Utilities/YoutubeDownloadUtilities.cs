using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;
using System.Runtime.InteropServices;
using System.Linq;

namespace GavinHomeApi.Utilities
{
    public class YoutubeDownloadUtilities
    {
        const string WORKING_DIR = "Working";
        const string YOUTUBE_DL_FILE_NAME = "youtube-dl";
        public static async Task<string> DownloadYoutubeVideoAsMp3(string urlToDownload)
        {
            var currentDir = PathUtilities.GetCurrentAssemblyDirectory();
            var workingDir = Path.Combine(currentDir, WORKING_DIR);
            if(!Directory.Exists(workingDir))
                Directory.CreateDirectory(workingDir);

            return await DownloadYoutubeVideoAsMp3(urlToDownload, workingDir);
        }

        public async static Task<string> DownloadYoutubeVideoAsMp3(string youtubeUrl, string outputDir)
        {
            string downloaderPath = YOUTUBE_DL_FILE_NAME;
            if(RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                downloaderPath = Path.Combine(PathUtilities.GetCurrentAssemblyDirectory(), YOUTUBE_DL_FILE_NAME);

            var args = $"--get-filename -o \"%(title)s.%(ext)s\" {youtubeUrl}";
            string videoName = await SystemUtilities.RunCommand(downloaderPath, args);
            string outputPath = Path.Combine(outputDir, Path.GetFileNameWithoutExtension(videoName) + ".mp3");
            Console.WriteLine(videoName);
            Console.WriteLine(outputPath);
            args = $"-x --audio-format mp3 {youtubeUrl} -o \"{outputPath}\"";
            //Console.WriteLine(args)
            await SystemUtilities.RunCommand(downloaderPath, args);
            if(File.Exists(outputPath))
                return outputPath;
            else
                throw new Exception("download faied, please check the log");

        }
        public async static Task DownloadYoutubeVideoListAsMp3(IEnumerable<string> urls, string outputDir)
        {
            HashSet<string> uniqueUrls = new HashSet<string>(urls, StringComparer.InvariantCultureIgnoreCase);

            if(!Directory.Exists(outputDir))
                Directory.CreateDirectory(outputDir);
            foreach (var url in urls)
            {
                await DownloadYoutubeVideoAsMp3(url, outputDir);                
            }
        }

        public async static Task<string> DownloadYoutubeVideoListAsZip(IEnumerable<string> urls)
        {
            string folderName = DateTime.Now.Ticks.ToString();
            var currentDir = PathUtilities.GetCurrentAssemblyDirectory();
            var workingDir = Path.Combine(currentDir, WORKING_DIR);
            string tempFolder = Path.Combine(workingDir, folderName);
            string zipOutput = Path.Combine(workingDir, folderName + ".zip");

            Directory.CreateDirectory(tempFolder);
            await DownloadYoutubeVideoListAsMp3(urls, tempFolder);
            ZipFile.CreateFromDirectory(tempFolder, zipOutput);
            Directory.Delete(tempFolder, true);
            return zipOutput;
        }
        public static async Task DownloadYoutubeVideoListAsMp3(string urls, string outputDir)
        {
            await DownloadYoutubeVideoListAsMp3(urls.Split(), outputDir);
        }

    }
}