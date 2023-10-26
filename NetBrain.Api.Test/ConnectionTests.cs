using System.Threading.Tasks;
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
		public async Task Connect_InvalidCredentials_ThrowsException()
		{
			using (var client = new Client("XXXXXXXXXXX", "XXXXXXXXXXX"))
			{
				_ = Assert.ThrowsAsync<AuthenticationException>(async () => await client.ConnectAsync().ConfigureAwait(false));
			}
		}

		[Fact]
		public async Task Connect_Succeeds()
		{
			using (var client = new Client(Config.Username, Config.Password))
			{
				await client.ConnectAsync().ConfigureAwait(false);
			}
		}
	}
}
