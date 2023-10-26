using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NetBrain.Api.Models;
using Newtonsoft.Json;

namespace NetBrain.Api
{
	public class Client : IDisposable
	{
		private readonly NetBrainCredential _credential;
		private readonly HttpClient _httpClient;
		private readonly DomainSelection _domainSelection;

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

		//ConnectAsync
		public async Task ConnectAsync()
		{
			await ConnectAsync(CancellationToken.None);
		}

		public async Task ConnectAsync(CancellationToken cancellationToken)
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

		//SetCurrentDomainAsync
		public async Task SetCurrentDomainAsync(DomainSelection domainSelection)
		{
			await SetCurrentDomainAsync(domainSelection, CancellationToken.None);
		}
		public async Task SetCurrentDomainAsync(
			DomainSelection domainSelection,
			CancellationToken cancellationToken)
		{
			await PutAsync<DomainSelection>(
				"Session/CurrentDomain",
				domainSelection,
				cancellationToken)
				.ConfigureAwait(false);
		}

		//GetSiteInfoAsync
		public async Task<List<SiteInfo>> GetSiteInfoAsync(string sitePath)
		{
			return await GetSiteInfoAsync(sitePath, CancellationToken.None);
		}

		public async Task<List<SiteInfo>> GetSiteInfoAsync(string sitePath, CancellationToken cancellationToken)
		{
			return (await GetAsync<SiteInfoResponse>("CMDB/Sites/SiteInfo", cancellationToken).ConfigureAwait(false)).Items;
		}

		//GetAllDomainsAsync
		public async Task<List<Domain>> GetAllDomainsAsync(Guid tenantId)
		{
			return await GetAllDomainsAsync(tenantId, CancellationToken.None);
		}

		public async Task<List<Domain>> GetAllDomainsAsync(Guid tenantId, CancellationToken cancellationToken)
		{
			return (await GetAsync<DomainResponse>($"CMDB/Domains?tenantId={tenantId}", cancellationToken).ConfigureAwait(false)).Items;
		}

		//GetAllAsync
		public async Task<List<T>> GetAllAsync<T>()
		{
			return await GetAllAsync<T>(CancellationToken.None);
		}
		public async Task<List<T>> GetAllAsync<T>(CancellationToken cancellationToken)
		{
			if (typeof(T).Name == nameof(Tenant))
			{
				return (await GetAsync<TenantResponse>("CMDB/Tenants", cancellationToken).ConfigureAwait(false)).Items as List<T>;
			}
			else
			{
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
