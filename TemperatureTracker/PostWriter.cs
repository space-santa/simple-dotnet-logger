using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Windows.Data.Json;

namespace TemperatureTracker
{
    class PostWriter : ITemperatureWriter
    {
        private static readonly HttpClient client = new HttpClient();

        public async Task Write(string value)
        {
            var json = new JsonObject();
            json.Add("device", JsonValue.CreateStringValue("BME280"));
            json.Add("timestamp", JsonValue.CreateStringValue(DateTime.UtcNow.ToLocalTime().ToString("yyyy-MM-ddTHH:mm:sszzz")));
            json.Add("temperature", JsonValue.CreateStringValue(value));

            var content = new StringContent(json.ToString(), Encoding.UTF8, "application/json");
            System.Diagnostics.Debug.WriteLine(content);
            var response = await client.PostAsync("http://whiterun:4567/api/temperature", content);
            //var response = await client.PostAsync("http://192.168.2.18:4567/api/temperature", content);
            var responseString = await response.Content.ReadAsStringAsync();
            System.Diagnostics.Debug.WriteLine(responseString);
        }
    }
}
