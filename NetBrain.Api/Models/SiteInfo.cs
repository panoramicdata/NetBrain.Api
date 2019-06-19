using System;
using System.Runtime.Serialization;

namespace NetBrain.Api.Models
{
	[DataContract]
	public class SiteInfo
	{
		[DataMember(Name = "siteId")]
		public Guid Id { get; set; }

		[DataMember(Name = "sitePath")]
		public string Path { get; set; }

		[DataMember(Name = "isContainer")]
		public bool IsContainer { get; set; }

		[DataMember(Name = "type")]
		public int Type { get; set; }
	}
}
