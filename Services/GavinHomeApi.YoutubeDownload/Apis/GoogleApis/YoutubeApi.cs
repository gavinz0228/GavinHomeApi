using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Upload;
using Google.Apis.Util.Store;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;

namespace GavinHomeApi.YoutubeDownload.Apis.GoogleApis
{
    public class YouTubeApi
    {
        protected static YouTubeService _youtubeService;
        
        public YouTubeApi()
        {
            Init();
        }
        public void Init()
        {
            _youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = "APIKEY",
                ApplicationName = "MyApp"
                 
            });
        }
        
        public async Task<List<string>> SearchVideos(string query)
        {
            List<string> results = new List<string>(); 
            var searchListRequest = _youtubeService.Search.List("snippet");
            searchListRequest.Q = query; 
            searchListRequest.MaxResults = 50;
            var searchListResponse = await searchListRequest.ExecuteAsync();

            return results;
        }
    }

}