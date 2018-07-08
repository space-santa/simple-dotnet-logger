using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CronLib;

namespace TemperatureTracker
{
    public class TemperatureJob<TheSensor, TheWriter> : CronJob 
        where TheSensor : ISensor, new() 
        where TheWriter : ITemperatureWriter, new()
    {
        protected override async Task Functionality()
        {
            await Task.Run(async () =>
            {
                var wrap = new TheSensor();
                var temperature = wrap.Temperature;
                ITemperatureWriter writer = new TheWriter();
                await writer.Write(temperature.ToString());
            }).ConfigureAwait(false);
        }
    }
    public static class TemperatureRunner
    {
        public static void Run()
        {
            CronRunner<TemperatureJob<BME280Wrapper, PostWriter>>.Run("0 0/1 * * * ?").GetAwaiter().GetResult();
        }
    }
}
