using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
namespace SmartSaver
{
    public static class Logger
    {
       
        public static void Log(string logMessage)
        {


            string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "logs.txt");

            File.AppendAllText(fileName, string.Format("Date: {0} Message: {1}\n\n", DateTime.Now, logMessage));
            
            
        }
    }
}
