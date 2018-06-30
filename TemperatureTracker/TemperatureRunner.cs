using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emmellsoft.IoT.Rpi.SenseHat;
using CronLib;

namespace TemperatureTracker
{
    public class TemperatureJob : CronJob
    {
        private readonly TimeSpan begin = new TimeSpan(7, 15, 0);
        private readonly TimeSpan end = new TimeSpan(23, 15, 0);

        private bool isInTime()
        {
            TimeSpan now = DateTime.Now.TimeOfDay;
            return now > begin && now < end;
        }

        protected override async Task Functionality()
        {
            await Task.Run(async () =>
            {
                var senseHat = await SenseHatFactory.GetSenseHat().ConfigureAwait(false);
                var wrap = new SenseHatWrapper(senseHat);
                wrap.ClearDisplay();
                var temperature = wrap.Temperature();
                if (isInTime())
                {
                    wrap.Write(temperature.ToString(), TemperatureColourMap.colourForTemperature(temperature), DisplayDirection.Deg180);
                }
                wrap.Sleep(TimeSpan.FromSeconds(15));
                wrap.ClearDisplay();
            }).ConfigureAwait(false);
        }
    }
    public static class TemperatureRunner
    {
        public static void Run()
        {
            CronRunner<TemperatureJob>.Run("0 0/1 * * * ?").GetAwaiter().GetResult();
        }
    }
}
