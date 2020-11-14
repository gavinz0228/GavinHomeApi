using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
namespace GavinHomeApi.Utilities
{
    public class PathUtilities
    {
        public static string GetCurrentAssemblyDirectory()
        {
            return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        } 
    }
}