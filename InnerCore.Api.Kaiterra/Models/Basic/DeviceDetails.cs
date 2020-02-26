using System;

namespace InnerCore.Api.Kaiterra.Models.Basic
{
    public class SensorReading
    {
        public string DeviceId { get; set; }

        public DeviceType DeviceType{ get; set; }

        /// <summary>
        /// Date and time when the sensor values have been recorded
        /// </summary>
        public DateTimeOffset? TimeStamp { get; set; }

        /// <summary>
        /// Pm2.5 value, measured in µg/m³
        /// </summary>
        public decimal? Pm25 { get; set; }

        /// <summary>
        /// Pm2.5 value, measured in µg/m³
        /// </summary>
        public decimal? Pm10 { get; set; }

        /// <summary>
        /// Relative humidity in %
        /// </summary>
        public decimal? RelativeHumidity { get; set; }

        /// <summary>
        /// Temperature in C°
        /// </summary>
        public decimal? Temperature { get; set; }

        /// <summary>
        /// TVOC in ppb
        /// </summary>
        public decimal? TotalVolatileOrganicCompounds { get; set; }

        /// <summary>
        /// CO2 in ppm
        /// </summary>
        public decimal? CO2 { get; set; }
    }

    public enum DeviceType
    {
        LaserEgg, SenseEdege
    }
}
