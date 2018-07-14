using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Diagnostics;

namespace TemperatureTracker
{
    public sealed class Config
    {
        private static readonly Config instance = new Config();

        public string Device { get; }
        public string Writer { get; }
        public string EndPoint { get; }
        public string CronJob { get; }

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static Config()
        {
        }

        private Config()
        {
            var resourceLoader = Windows.ApplicationModel.Resources.ResourceLoader.GetForCurrentView();
            Device = resourceLoader.GetString("Device");
            Writer = resourceLoader.GetString("Writer");
            EndPoint = resourceLoader.GetString("EndPoint");
            CronJob = resourceLoader.GetString("CronJob");
        }

        public static Config Instance
        {
            get
            {
                return instance;
            }
        }
    }
}
