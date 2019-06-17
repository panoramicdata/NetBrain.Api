using System;
using System.Runtime.Serialization;

namespace NetBrain.Api.Models
{
	[DataContract]
	public class Domain
	{
		[DataMember(Name = "domainId")]
		public Guid Id { get; set; }

		[DataMember(Name = "domainName")]
		public string Name { get; set; }
	}
}
