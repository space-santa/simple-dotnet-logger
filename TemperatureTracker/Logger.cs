using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace TemperatureTracker
{
    internal sealed class Logger
    {
        private static readonly Logger instance = new Logger();
        private StorageFile theLog;
        public Boolean UseConsole { get; set; } = true;

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static Logger()
        {
        }

        private Logger()
        {
            System.Diagnostics.Debug.WriteLine("Hello");
            Task.Run(() => init()).Wait();
        }

        private async Task init()
        {
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            theLog = await storageFolder.CreateFileAsync("the.log", CreationCollisionOption.OpenIfExists);
            //useConsole = Config.Instance.Logger == "Console";
            System.Diagnostics.Debug.WriteLine(theLog.Path);
        }

        public static Logger Instance
        {
            get
            {
                return instance;
            }
        }

        public void LogToFile(string message)
        {
            Task.Run(() => FileIO.AppendTextAsync(theLog, message));
        }

        public void LogToConsole(string message)
        {
            System.Diagnostics.Debug.WriteLine(message);
        }

        public void Log(string what)
        {
            var timestamp = DateTime.UtcNow.ToLocalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffzzz");
            var message = timestamp + " " + what + "\n";
            if (UseConsole)
            {
                LogToConsole(message);
            }
            else
            {
                LogToFile(message);
            }
        }
    }
}
