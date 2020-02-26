using System;
using System.Runtime.Serialization;

namespace InnerCore.Api.Kaiterra.Models.Basic
{
    [DataContract]
    public class AqiInfo
    {
        [DataMember(Name = "ts")]
        public DateTimeOffset TimeStamp { get; set; }

        [DataMember(Name = "data")]
        public LaserEggData Data { get; set; }
    }
}