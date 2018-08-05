using System;
using System.IO;
using System.Threading.Tasks;

namespace Logger
{
    public sealed class Log
    {
        private static readonly Log instance = new Log();
        private string theLogPath;
        public Boolean UseConsole { get; set; } = true;

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static Log()
        {
        }

        private Log()
        {
            init();
        }

        private void init()
        {
            string storageFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            theLogPath = storageFolder + "/the.log";
            System.Diagnostics.Debug.WriteLine(theLogPath);
        }

        public static Log Instance
        {
            get
            {
                return instance;
            }
        }

        public void LogToFile(string message)
        {
            Task.Run(() => File.AppendAllText(theLogPath, message));
        }

        public void LogToConsole(string message)
        {
            System.Diagnostics.Debug.WriteLine(message);
        }

        public void Write(string what)
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
