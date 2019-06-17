using NetBrain.Api.Interfaces;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace NetBrain.Api.Models
{
	[DataContract]
	public class TenantResponse : Response, IItemList<Tenant>
	{
		[DataMember(Name = "tenants")]
		public List<Tenant> Items { get; set; }
	}
}