using System.Runtime.Serialization;

namespace InnerCore.Api.Kaiterra.Models.Basic
{
    [DataContract]
    public class SenseEdge
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "latest")]
        public SenseEdgeData Data { get; set; }
    }
}