using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemperatureTracker
{
    class MockSensor : ISensor
    {
        public double Temperature
        {
            get
            {
                return 12.34;
            }
        }
    }
}
