using System.Runtime.Serialization;

namespace InnerCore.Api.Kaiterra.Models.Extended
{
    [DataContract]
    public class History
    {
        [DataMember(Name = "data")]
        public Data[] Data { get; set; }

        [DataMember(Name = "units")]
        public Units Units { get; set; }

        [DataMember(Name = "info")]
        public Info Info { get; set; }
    }
}
