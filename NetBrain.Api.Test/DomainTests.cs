using System;
using Xunit;
using Xunit.Abstractions;

namespace NetBrain.Api.Test
{
	public class DomainTests : TestWithOutput
	{
		public DomainTests(ITestOutputHelper iTestOutputHelper) : base(iTestOutputHelper, ConnectionMode.Connect)
		{
		}

		[Fact]
		public async void GetDomains_Succeeds()
		{
			var items = await Client
				.GetAllDomainsAsync(Config.TenantId)
				.ConfigureAwait(false);
			Assert.NotNull(items);
			Assert.NotEmpty(items);
			Assert.All(items, t =>
			{
				Assert.NotNull(t);
				Assert.NotEqual(Guid.Empty, t.Id);
				Assert.NotNull(t.Name);
			});
		}
	}
}
