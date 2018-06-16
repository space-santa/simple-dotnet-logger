using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emmellsoft.IoT.Rpi.SenseHat;

namespace TemperatureTracker
{
    public static class TemperatureRunner
    {
        public static void Run()
        {
            Task.Run(async () =>
            {
                while (true)
                {
                    Debug.WriteLine("Start running");
                    var senseHat = await SenseHatFactory.GetSenseHat().ConfigureAwait(false);
                    Debug.WriteLine("Got a head");
                    var wrap = new SenseHatWrapper(senseHat);
                    wrap.ClearDisplay();
                    var temperature = wrap.Temperature();
                    wrap.Write(temperature.ToString(), TemperatureColourMap.colourForTemperature(temperature), DisplayDirection.Deg180);
                    wrap.Sleep(TimeSpan.FromSeconds(5));
                    wrap.ClearDisplay();
                    wrap.Sleep(TimeSpan.FromSeconds(25));
                }
            }).ConfigureAwait(false);
        }
    }
}
