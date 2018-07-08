using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemperatureTracker
{
    class BME280Wrapper : ISensor
    {
        private static BME280Sensor BME280 = null;

        public BME280Wrapper()
        {
            if (BME280 == null)
            {
                BME280 = new BME280Sensor();
                Task.Run(() => BME280.Initialize()).Wait();
            }
        }

        public double Temperature
        {
            get
            {
                double temperature = Task.Run(() => BME280.ReadTemperature()).Result;
                return temperature;
            }
        }
    }
}
