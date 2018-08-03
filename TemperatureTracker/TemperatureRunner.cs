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
            switch (Config.Instance.Writer)
            {
                case "Post":
                    RunWithWriter<PostWriter>();
                    break;
                case "Console":
                    RunWithWriter<ConsoleWriter>();
                    break;
                default:
                    break;
            }
        }

        private static void RunWithWriter<TheWriter>() where TheWriter : ITemperatureWriter, new()
        {
            switch (Config.Instance.Device)
            {
                case "SenseHat":
                    CronRunner<TemperatureJob<SenseHatWrapper, TheWriter>>.Run(Config.Instance.CronJob).GetAwaiter().GetResult();
                    break;
                case "BME280":
                    CronRunner<TemperatureJob<BME280Wrapper, TheWriter>>.Run(Config.Instance.CronJob).GetAwaiter().GetResult();
                    break;
                case "Mock":
                    CronRunner<TemperatureJob<MockSensor, TheWriter>>.Run(Config.Instance.CronJob).GetAwaiter().GetResult();
                    break;
                default:
                    break;
            }
        }

        private static void RunWithWriterAndSensor<TheSensor, TheWriter>()
            where TheSensor : ISensor, new()
            where TheWriter : ITemperatureWriter, new()
        {
            Logger.Instance.Log("Get Started");
            CronRunner<TemperatureJob<TheSensor, TheWriter>>.Run(Config.Instance.CronJob).GetAwaiter().GetResult();
        }
    }
}
