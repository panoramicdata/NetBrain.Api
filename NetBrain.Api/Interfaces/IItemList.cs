using System.Collections.Generic;

namespace NetBrain.Api.Interfaces
{
	internal interface IItemList<T>
	{
		List<T> Items { get; set; }
	}
}