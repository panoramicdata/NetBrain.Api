using NetBrain.Api.Exceptions;
using Xunit;
using Xunit.Abstractions;

namespace NetBrain.Api.Test
{
	public class ConnectionTests : TestWithOutput
	{
		public ConnectionTests(ITestOutputHelper iTestOutputHelper) : base(iTestOutputHelper, ConnectionMode.None)
		{
		}

		[Fact]
		public async void Connect_InvalidCredentials_ThrowsException()
		{
			using (var client = new Client("XXXXXXXXXXX", "XXXXXXXXXXX"))
			{
				var exception = Assert.ThrowsAsync<AuthenticationException>(async () => await client.ConnectAsync().ConfigureAwait(false));
			}
		}

		[Fact]
		public async void Connect_Succeeds()
		{
			using (var client = new Client(Config.Username, Config.Password))
			{
				await client.ConnectAsync().ConfigureAwait(false);
			}
		}
	}
}
