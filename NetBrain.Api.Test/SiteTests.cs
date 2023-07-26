using System;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace NetBrain.Api.Test
{
	public class SiteTests : TestWithOutput
	{
		public SiteTests(ITestOutputHelper iTestOutputHelper) : base(iTestOutputHelper, ConnectionMode.ConnectAndSelectDomain)
		{
		}

		[Fact]
		public async Task GetSites_Succeeds()
		{
			var items = await Client
				.GetSiteInfoAsync("My Network")
				.ConfigureAwait(false);
			Assert.NotNull(items);
			Assert.All(items, item =>
			{
				Assert.NotNull(item);
				Assert.NotEqual(Guid.Empty, item.Id);
				Assert.NotNull(item.Path);
				Assert.NotEqual(0, item.Type);
				Assert.True(item.IsContainer);
			});
		}
	}
}
