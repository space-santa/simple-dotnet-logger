using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logger;

namespace TemperatureTracker
{
    class ConsoleWriter : ITemperatureWriter
    {
        public async Task Write(string value)
        {
            Log.Instance.Write(value);
            System.Diagnostics.Debug.WriteLine(value);
        }
    }
}
