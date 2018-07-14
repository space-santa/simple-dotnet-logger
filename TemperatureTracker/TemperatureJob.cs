using System.Threading.Tasks;
using CronLib;

namespace TemperatureTracker
{
    public class TemperatureJob<TheSensor, TheWriter> : CronJob 
        where TheSensor : ISensor, new() 
        where TheWriter : ITemperatureWriter, new()
    {
        protected override async Task Functionality()
        {
            await Task.Run(async () =>
            {
                var wrap = new TheSensor();
                var temperature = wrap.Temperature;
                ITemperatureWriter writer = new TheWriter();
                await writer.Write(temperature.ToString());
            }).ConfigureAwait(false);
        }
    }
}
