using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;

namespace TemperatureTracker
{
    class TemperatureColourMap
    {
        public static Color colourForTemperature(double temperature)
        {
            if (temperature < 10)
            {
                return Colors.DarkBlue;
            }

            if (temperature < 15)
            {
                return Colors.Blue;
            }

            if (temperature < 20)
            {
                return Colors.Green;
            }

            if (temperature < 25)
            {
                return Colors.Yellow;
            }

            if (temperature < 30)
            {
                return Colors.Orange;
            }

            if (temperature < 35)
            {
                return Colors.Red;
            }

            return Colors.HotPink;
        }
    }
}
