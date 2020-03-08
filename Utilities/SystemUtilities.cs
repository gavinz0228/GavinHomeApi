using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.InteropServices;
namespace GavinHomeApi.Utilities
{
    public class SystemUtilities
    {
        public static async Task<string> RunCommand(string programPath, string arguments)
        {
                var output = await Task<string>.Factory.StartNew(()=>{
                var process  = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = programPath,
                        Arguments = arguments,
                        RedirectStandardOutput = true
                    }
                };
                process.Start();
                string output = process.StandardOutput.ReadToEnd();
                process.WaitForExit();
                return output;
            });
            return output;
        } 
    }
}