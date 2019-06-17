using NetBrain.Api.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NetBrain.Api
{
	public class Client : IDisposable
	{
		private readonly NetBrainCredential _credential;
		private readonly HttpClient _httpClient;
		private DomainSelection _domainSelection;

		public Client(string username, string password)
		{
			_credential = new NetBrainCredential
			{
				Username = username,
				Password = password
			};

			_httpClient = new HttpClient
			{
				BaseAddress = new Uri("https://ite.netbraintech.com/ServicesAPI/API/V1/")
			};
		}

		public Client(string username, string password, Guid tenantId, Guid domainId)
			: this(username, password)
			=> _domainSelection = new DomainSelection { TenantId = tenantId, DomainId = domainId };

		public async Task ConnectAsync(CancellationToken cancellationToken = default)
		{
			var tokenResponse = await PostAsync<TokenResponse>("Session", _credential, cancellationToken)
				.ConfigureAwait(false);
			_httpClient.DefaultRequestHeaders.Add("Token", tokenResponse.Token);

			// Once connected, the credential values are destroyed
			_credential.ClearPassword();

			if (_domainSelection != null)
			{
				await SetCurrentDomainAsync(_domainSelection).ConfigureAwait(false);
			}
		}

		public async Task SetCurrentDomainAsync(
			DomainSelection domainSelection,
			CancellationToken cancellationToken = default)
		{
			await PutAsync<DomainSelection>(
				"Session/CurrentDomain",
				domainSelection,
				cancellationToken)
				.ConfigureAwait(false);
		}

		public async Task<List<Domain>> GetAllDomainsAsync(Guid tenantId, CancellationToken cancellationToken = default)
		{
			return (await GetAsync<DomainResponse>($"CMDB/Domains?tenantId={tenantId}", cancellationToken).ConfigureAwait(false)).Items;
		}

		public async Task<List<T>> GetAllAsync<T>(CancellationToken cancellationToken = default)
		{
			switch (typeof(T).Name)
			{
				case nameof(Tenant):
					return (await GetAsync<TenantResponse>("CMDB/Tenants", cancellationToken).ConfigureAwait(false)).Items as List<T>;
				default:
					throw new NotSupportedException($"Type {typeof(T).Name} not supported.");
			}
		}

		private async Task<T> PostAsync<T>(string subUrl, object bodyObject, CancellationToken cancellationToken) where T : Response
		{
			var bodyContent = new StringContent(JsonConvert.SerializeObject(bodyObject), Encoding.UTF8);
			var response = await _httpClient
				.PostAsync(subUrl, bodyContent, cancellationToken)
				.ConfigureAwait(false);

			var responseBody = await response
				.Content
				.ReadAsStringAsync()
				.ConfigureAwait(false);

			if (responseBody == null)
			{
				return default;
			}
			// We have a body

			var responseObject = JsonConvert.DeserializeObject<T>(responseBody);

			return responseObject;
		}

		private async Task<Response> PutAsync<T>(string subUrl, object bodyObject, CancellationToken cancellationToken)
		{
			var bodyContent = new StringContent(JsonConvert.SerializeObject(bodyObject), Encoding.UTF8);
			var response = await _httpClient
				.PutAsync(subUrl, bodyContent, cancellationToken)
				.ConfigureAwait(false);

			var responseBody = await response
				.Content
				.ReadAsStringAsync()
				.ConfigureAwait(false);

			if (responseBody == null)
			{
				return default;
			}
			// We have a body

			var responseObject = JsonConvert.DeserializeObject<Response>(responseBody);

			return responseObject;
		}

		private async Task<T> GetAsync<T>(string subUrl, CancellationToken cancellationToken) where T : Response
		{
			var response = await _httpClient
				.GetAsync(subUrl, cancellationToken)
				.ConfigureAwait(false);

			var responseBody = await response
				.Content
				.ReadAsStringAsync()
				.ConfigureAwait(false);

			if (responseBody == null)
			{
				return default;
			}
			// We have a body

			var responseObject = JsonConvert.DeserializeObject<T>(responseBody);

			return responseObject;
		}



		public void Dispose()
		{
			_httpClient.Dispose();
		}
	}
}
