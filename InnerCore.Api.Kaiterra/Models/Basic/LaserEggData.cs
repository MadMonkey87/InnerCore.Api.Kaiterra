using System.Runtime.Serialization;

namespace InnerCore.Api.Kaiterra.Models.Basic
{
    [DataContract]
    public class LaserEggData
    {
        [DataMember(Name = "pm25")]
        public decimal? Pm25 { get; set; }

        [DataMember(Name = "pm10")]
        public decimal? Pm10 { get; set; }

        [DataMember(Name = "humidity")]
        public decimal? RelativeHumidity { get; set; }

        [DataMember(Name = "temp")]
        public decimal? Temperature { get; set; }

        [DataMember(Name = "rtvoc")]
        public decimal? TotalVolatileOrganicCompounds { get; set; }
    }
}