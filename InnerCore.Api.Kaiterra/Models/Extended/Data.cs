using System;
using System.Runtime.Serialization;

namespace InnerCore.Api.Kaiterra.Models.Extended
{
    [DataContract]
    public class Data
    {
        [DataMember(Name = "ts")]
        public DateTimeOffset? TimeStamp { get; set; }

        [DataMember(Name = "pollutants")]
        public Pollutants Pollutants { get; set; }

        [DataMember(Name = "info")]
        public Info Info { get; set; }
    }
}
