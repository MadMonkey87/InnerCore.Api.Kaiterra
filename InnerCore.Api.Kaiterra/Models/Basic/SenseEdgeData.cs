using System;
using System.Runtime.Serialization;

namespace InnerCore.Api.Kaiterra.Models.Basic
{
    [DataContract]
    public class SenseEdgeData
    {
        [DataMember(Name = "ts")]
        public DateTimeOffset TimeStamp { get; set; }

        [DataMember(Name = "km100.rpm25c")]
        public decimal? Pm25 { get; set; }

        [DataMember(Name = "km100.rpm10c")]
        public decimal? Pm10 { get; set; }

        [DataMember(Name = "km102.rhumid")]
        public decimal? RelativeHumidity { get; set; }

        [DataMember(Name = "km102.rtemp")]
        public decimal? Temperature { get; set; }

        [DataMember(Name = "km102.rtvoc (ppb)")]
        public decimal? TotalVolatileOrganicCompounds { get; set; }

        [DataMember(Name = "rco2 (ppm)")]
        public decimal? CO2 { get; set; }
    }
}