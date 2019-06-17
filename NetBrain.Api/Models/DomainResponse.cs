using NetBrain.Api.Interfaces;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace NetBrain.Api.Models
{

	[DataContract]
	public class DomainResponse : Response, IItemList<Domain>
	{
		[DataMember(Name = "domains")]
		public List<Domain> Items { get; set; }
	}
}