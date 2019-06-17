using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Reflection;

namespace NetBrain.Api.Test
{

	internal class TestPortalConfig
	{
		public static IConfigurationRoot Configuration { get; set; }
		public string Username { get; }
		public string Password { get; }
		public Guid TenantId { get; set; }
		public Guid DomainId { get; set; }

		internal TestPortalConfig(ILogger logger)
		{
			var location = typeof(TestPortalConfig).GetTypeInfo().Assembly.Location;
			var dirPath = Path.Combine(Path.GetDirectoryName(location), "../../..");
			var builder = new ConfigurationBuilder()
				.SetBasePath(dirPath)
				.AddJsonFile("appsettings.json");
			Configuration = builder.Build();

			Username = Configuration["Config:Username"];
			Password = Configuration["Config:Password"];
			TenantId = new Guid(Configuration["Config:TenantId"]);
			DomainId = new Guid(Configuration["Config:DomainId"]);
		}
	}
}