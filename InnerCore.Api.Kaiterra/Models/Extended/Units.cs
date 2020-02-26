using System;
using System.Runtime.Serialization;

namespace InnerCore.Api.Kaiterra.Models.Extended
{
    [DataContract]
    public class Units
    {
        [DataMember(Name = "aqi")]
        public String AirQualityIndex { get; set; }

        [DataMember(Name = "rco2")]
        public String CO2 { get; set; }

        [DataMember(Name = "rhumid")]
        public String RelativeHumidity { get; set; }

        [DataMember(Name = "rpm10c")]
        public String Pm10 { get; set; }

        [DataMember(Name = "rpm25c")]
        public String Pm25 { get; set; }

        [DataMember(Name = "rtemp")]
        public String Temperature { get; set; }

        [DataMember(Name = "rtvoc")]
        public String TotalVolatileOrganicCompounds { get; set; }
    }
}
