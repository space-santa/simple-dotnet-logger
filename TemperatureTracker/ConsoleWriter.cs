using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemperatureTracker
{
    class ConsoleWriter : ITemperatureWriter
    {
        public async Task Write(string value)
        {
            System.Diagnostics.Debug.WriteLine(value);
        }
    }
}
