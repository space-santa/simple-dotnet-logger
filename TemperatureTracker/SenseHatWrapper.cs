using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI;

using Emmellsoft.IoT.Rpi.SenseHat;
using Emmellsoft.IoT.Rpi.SenseHat.Fonts.SingleColor;

namespace TemperatureTracker
{
    public class NoSensorDataException : Exception
    {
        public NoSensorDataException() : base() { }
        public NoSensorDataException(string message) : base(message) { }
        public NoSensorDataException(string message, Exception inner) : base(message, inner) { }
    }

    class SenseHatWrapper : ISensor
    {
        private readonly ISenseHat SenseHat;
        private readonly ManualResetEventSlim _waitEvent = new ManualResetEventSlim(false);

        private readonly TimeSpan begin = new TimeSpan(7, 15, 0);
        private readonly TimeSpan end = new TimeSpan(23, 15, 0);

        private bool IsInTime()
        {
            TimeSpan now = DateTime.Now.TimeOfDay;
            return now > begin && now < end;
        }

        public void ClearDisplay()
        {
            SenseHat.Display.Clear();
            SenseHat.Display.Update();
        }

        public SenseHatWrapper()
        {
            var senseHat = Task.Run(SenseHatFactory.GetSenseHat).Result;
            SenseHat = senseHat;
            ClearDisplay();
        }

        public double Temperature
        {
            get
            {
                for (int i = 0; i < 20; ++i)
                {
                    SenseHat.Sensors.HumiditySensor.Update();
                    var t = SenseHat.Sensors.Temperature;

                    if (t.HasValue)
                    {
                        var temperature = Math.Round((double)t, 1);
                        Task.Run(() => WriteTemperatureAsync(temperature));
                        return temperature;
                    }
                    else
                    {
                        Sleep(TimeSpan.FromSeconds(0.5));
                    }
                }
                throw new NoSensorDataException("No Temperature");
            }
        }

        private void WriteTemperatureAsync(double temperature)
        {
            if (IsInTime())
            {
                Write(temperature.ToString(), TemperatureColourMap.colourForTemperature(temperature), DisplayDirection.Deg180);
                Sleep(TimeSpan.FromSeconds(15));
                ClearDisplay();
            }
        }

        public void Write(string text, Color color, DisplayDirection rotation = DisplayDirection.Deg0)
        {
            SenseHat.Display.Reset();
            SenseHat.Display.Direction = rotation;

            if (text.Length > 2)
            {
                // Too long to fit the display!
                text = text.Substring(0, 2);
            }

            var tinyFont = new TinyFont();

            SenseHat.Display.Clear();
            tinyFont.Write(SenseHat.Display, text, color);
            SenseHat.Display.Update();
        }

        public void Sleep(TimeSpan duration)
        {
            _waitEvent.Wait(duration);
        }
    }
}
