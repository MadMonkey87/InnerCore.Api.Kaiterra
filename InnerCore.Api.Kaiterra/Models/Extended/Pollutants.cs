using System.Runtime.Serialization;

namespace InnerCore.Api.Kaiterra.Models.Extended
{
    [DataContract]
    public class Pollutants
    {
        [DataMember(Name = "aqi")]
        public PollutantValue AirQualityIndex { get; set; }

        [DataMember(Name = "rco2")]
        public PollutantValue CO2 { get; set; }

        [DataMember(Name = "rhumid")]
        public PollutantValue RelativeHumidity { get; set; }

        [DataMember(Name = "rpm10c")]
        public PollutantValue Pm10 { get; set; }

        [DataMember(Name = "rpm25c")]
        public PollutantValue Pm25 { get; set; }

        [DataMember(Name = "rtemp")]
        public PollutantValue Temperature { get; set; }

        [DataMember(Name = "rtvoc")]
        public PollutantValue TotalVolatileOrganicCompounds { get; set; }
    }

    [DataContract]
    public class PollutantValue
    {
        [DataMember(Name = "aqi")]
        public int? AirQualityIndex { get; set; }

        [DataMember(Name = "value")]
        public decimal? Value { get; set; }
    }
}
