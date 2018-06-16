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

    class SenseHatWrapper
    {
        private readonly ISenseHat SenseHat;
        private readonly ManualResetEventSlim _waitEvent = new ManualResetEventSlim(false);

        public void ClearDisplay()
        {
            SenseHat.Display.Clear();
            SenseHat.Display.Update();
        }

        public SenseHatWrapper(ISenseHat senseHat)
        {
            SenseHat = senseHat;
        }

        public double Temperature()
        {
            for (int i = 0; i < 20; ++i)
            {
                SenseHat.Sensors.HumiditySensor.Update();
                var t = SenseHat.Sensors.Temperature;

                if (t.HasValue)
                {
                    return Math.Round((double)t, 1);
                }
                else
                {
                    Sleep(TimeSpan.FromSeconds(0.5));
                }
            }
            throw new NoSensorDataException("No Temperature");
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
