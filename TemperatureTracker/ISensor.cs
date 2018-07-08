using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemperatureTracker
{
    public interface ISensor
    {
        double Temperature { get; }
    }
}
