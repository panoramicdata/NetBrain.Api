﻿using System;
using System.Threading.Tasks;
using NetBrain.Api.Models;
using Xunit;
using Xunit.Abstractions;

namespace NetBrain.Api.Test
{
	public class TenantTests : TestWithOutput
	{
		public TenantTests(ITestOutputHelper iTestOutputHelper) : base(iTestOutputHelper, ConnectionMode.Connect)
		{
		}

		[Fact]
		public async Task GetTenants_Succeeds()
		{
			var tenants = await Client
				.GetAllAsync<Tenant>()
				.ConfigureAwait(false);
			Assert.NotNull(tenants);
			Assert.NotEmpty(tenants);
			Assert.All(tenants, t =>
			{
				Assert.NotNull(t);
				Assert.NotEqual(Guid.Empty, t.Id);
				Assert.NotNull(t.Name);
			});
		}
	}
}
