using System;
using System.Runtime.Serialization;

namespace NetBrain.Api.Models
{
	[DataContract]
	public class Tenant
	{
		[DataMember(Name = "tenantId")]
		public Guid Id { get; set; }

		[DataMember(Name = "tenantName")]
		public string Name { get; set; }
	}
}
