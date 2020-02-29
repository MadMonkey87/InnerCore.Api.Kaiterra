using System.Runtime.Serialization;

namespace InnerCore.Api.Kaiterra.Models.Extended
{
	[DataContract]
	public class Battery
	{
		[DataMember(Name = "charging")]
		public bool? Charging { get; set; }

		[DataMember(Name = "level")]
		public int? Level { get; set; }
	}
}
