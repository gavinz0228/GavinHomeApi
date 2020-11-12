using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
namespace GavinHomeApi.Utilities
{
    public class FileUtilities
    {
        public async static Task<byte[]> ReadAllBytes(string filePath)
        {
            byte[] buffer;
            using(FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                buffer = new byte[stream.Length];
                await stream.ReadAsync(buffer, 0 ,(int)stream.Length);
            }
            return buffer;
        }
    }
}