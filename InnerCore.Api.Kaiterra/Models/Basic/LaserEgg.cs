using System.Runtime.Serialization;

namespace InnerCore.Api.Kaiterra.Models.Basic
{
    [DataContract]
    public class LaserEgg
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "info.aqi")]
        public AqiInfo Aqi { get; set; }
    }
}
