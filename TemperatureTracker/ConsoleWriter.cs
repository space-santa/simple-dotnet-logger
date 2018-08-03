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
            Logger.Instance.Log(value);
            System.Diagnostics.Debug.WriteLine(value);
        }
    }
}
