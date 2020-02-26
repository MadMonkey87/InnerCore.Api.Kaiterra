using System;
using System.Linq;
using System.Threading.Tasks;
using InnerCore.Api.Kaiterra.Models.Extended;

namespace InnerCore.Api.Kaiterra.ExtendedSample
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            Console.WriteLine("please enter your email address of your kaiterra account");
            var email = Console.ReadLine();
            Console.WriteLine("please enter your password");
            var password = Console.ReadLine();

            var client = new KaiterraExtendedClient();
            await client.Login(email, password);

            var devices = await client.GetDevices();

            Console.WriteLine($"found {devices.Count()} device(s)");

            if (!devices.Any())
            {
                Console.WriteLine("Press enter to quit");
                Console.ReadLine();
                return;
            }

            foreach (var device in devices)
            {
                Console.WriteLine($"    {device.Name}");
            }

            Console.WriteLine($"current values for '{devices.First().Name}'");
            var history = await client.GetCurrentValues(devices.First().Id);
            PlotValues(history);

            history = await client.GetHistoricValues(devices.First().Id, DateTimeOffset.Now.AddDays(-30),
                DateTimeOffset.Now, TimeSpan.FromMinutes(15));
            Console.WriteLine($"there have been {history.Data.Length} recorded values for the last 30 days (15 minutes interval)");

            Console.WriteLine("Press enter to quit");
            Console.ReadLine();
        }

        private static void PlotValues(History history)
        {
            var data = history.Data.First();

            Console.WriteLine($"   time stamp: {data.TimeStamp}");

            if (data.Pollutants.AirQualityIndex.Value.HasValue)
            {
                Console.WriteLine($"   Air Quality Index: {data.Pollutants.AirQualityIndex.Value}");
            }

            if (data.Pollutants.CO2.Value.HasValue)
            {
                Console.WriteLine($"   CO2: {data.Pollutants.CO2.Value} {history.Units.CO2}");
            }

            if (data.Pollutants.Pm25.Value.HasValue)
            {
                Console.WriteLine($"   Pm2.5: {data.Pollutants.Pm25.Value} {history.Units.Pm25}");
            }

            if (data.Pollutants.Pm10.Value.HasValue)
            {
                Console.WriteLine($"   Pm10: {data.Pollutants.Pm10.Value} {history.Units.Pm10}");
            }

            if (data.Pollutants.TotalVolatileOrganicCompounds.Value.HasValue)
            {
                Console.WriteLine($"   tVOC: {data.Pollutants.TotalVolatileOrganicCompounds.Value} {history.Units.TotalVolatileOrganicCompounds}");
            }
        }
    }
}
