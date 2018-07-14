using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CronLib;

namespace TemperatureTracker
{
    public static class TemperatureRunner
    {
        public static void Run()
        {
            switch (Config.Instance.Device)
            {
                case "SenseHat":
                    CronRunner<TemperatureJob<SenseHatWrapper, PostWriter>>.Run(Config.Instance.CronJob).GetAwaiter().GetResult();
                    break;
                case "BME280":
                    CronRunner<TemperatureJob<BME280Wrapper, PostWriter>>.Run(Config.Instance.CronJob).GetAwaiter().GetResult();
                    break;
                default:
                    break;
            }
        }
    }
}
