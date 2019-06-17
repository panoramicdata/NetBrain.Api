using System;
using System.Runtime.Serialization;

namespace NetBrain.Api.Models
{
	[DataContract]
	public class DomainSelection
	{
		[DataMember(Name = "tenantId")]
		public Guid TenantId { get; set; }

		[DataMember(Name = "domainId")]
		public Guid DomainId { get; set; }
	}
}
