using NetBrain.Api.Interfaces;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace NetBrain.Api.Models
{

	[DataContract]
	public class SiteInfoResponse : Response, IItemList<SiteInfo>
	{
		[DataMember(Name = "siteInfo")]
		public List<SiteInfo> Items { get; set; }
	}
}