using System;
using System.Threading.Tasks;
using InnerCore.Api.Kaiterra.Models.Basic;

namespace InnerCore.Api.Kaiterra.BasicSample
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            Console.WriteLine("please enter your api key");
            var apiKey = Console.ReadLine();

            var client = new KaiterraBasicClient(apiKey);

            PlotSensorReading(await client.GetLaserEggDetails("00000000-0001-0001-0000-00007e57c0de"));

            PlotSensorReading(await client.GetSenseEdgeDetails("00000000-0031-0001-0000-00007e57c0de"));

            Console.WriteLine("Press enter to quit");
            Console.ReadLine();
        }

        private static void PlotSensorReading(SensorReading sensorReading)
        {
            Console.WriteLine($"deviceId: {sensorReading.DeviceId}");
            Console.WriteLine($"   type: {sensorReading.DeviceType}");
            Console.WriteLine($"   time stamp: {sensorReading.TimeStamp}");

            if (sensorReading.CO2.HasValue)
            {
                Console.WriteLine($"   CO2: {sensorReading.CO2} ppm");
            }
            else
            {
                Console.WriteLine("   CO2: -");
            }


            if (sensorReading.Pm10.HasValue)
            {
                Console.WriteLine($"   Pm10: {sensorReading.Pm10} µg/m³");
            }
            else
            {
                Console.WriteLine("   Pm10: -");
            }

            if (sensorReading.Pm25.HasValue)
            {
                Console.WriteLine($"   Pm2.5: {sensorReading.Pm25} µg/m³");
            }
            else
            {
                Console.WriteLine("   Pm2.5: -");
            }

            if (sensorReading.TotalVolatileOrganicCompounds.HasValue)
            {
                Console.WriteLine($"   tVOC: {sensorReading.TotalVolatileOrganicCompounds} ppb");
            }
            else
            {
                Console.WriteLine("   tVOC: -");
            }

            if (sensorReading.RelativeHumidity.HasValue)
            {
                Console.WriteLine($"   relative humidity: {sensorReading.RelativeHumidity} %");
            }
            else
            {
                Console.WriteLine("   relative humidity: -");
            }

            if (sensorReading.Temperature.HasValue)
            {
                Console.WriteLine($"   temperature: {sensorReading.Temperature} C°");
            }
            else
            {
                Console.WriteLine("   temperature: -");
            }
        }
    }
}
