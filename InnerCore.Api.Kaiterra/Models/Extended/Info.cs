using System.Runtime.Serialization;

namespace InnerCore.Api.Kaiterra.Models.Extended
{
	[DataContract]
	public class Info
	{
		[DataMember(Name = "battery")]
		public Battery Battery { get; set; }
	}
}
