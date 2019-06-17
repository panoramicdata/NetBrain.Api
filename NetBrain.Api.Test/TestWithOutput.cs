using Microsoft.Extensions.Logging;
using System;
using Xunit.Abstractions;

namespace NetBrain.Api.Test
{
	public abstract class TestWithOutput : IDisposable
	{
		protected ILogger Logger { get; }
		internal TestPortalConfig Config { get; }
		public Client Client { get; }

		protected TestWithOutput(ITestOutputHelper iTestOutputHelper, ConnectionMode connectionMode)
		{
			Logger = iTestOutputHelper.BuildLogger();

			Config = new TestPortalConfig(Logger);

			switch(connectionMode)
			{
				case ConnectionMode.None:
					break;
				case ConnectionMode.Connect:
					Client = new Client(Config.Username, Config.Password);
					Client.ConnectAsync().GetAwaiter().GetResult();
					break;
				case ConnectionMode.ConnectAndSelectDomain:
					Client = new Client(Config.Username, Config.Password, Config.TenantId, Config.DomainId);
					Client.ConnectAsync().GetAwaiter().GetResult();
					break;
			}
		}

		public void Dispose()
		{
			Client?.Dispose();
		}
	}
}