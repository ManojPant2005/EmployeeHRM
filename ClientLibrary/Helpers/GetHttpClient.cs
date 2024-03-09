using BaseLibrary.DTOs;

namespace ClientLibrary.Helpers
{
    public class GetHttpClient
    {
        public GetHttpClient(IHttpClientFactory httpClientFactory, LocalStorageService localStorageService)
        {
            this.httpClientFactory = httpClientFactory;
            this.localStorageService = localStorageService;
        }
        private const string HeaderKey = "Authorization";
        private readonly IHttpClientFactory httpClientFactory;
        private readonly LocalStorageService localStorageService;

        public async Task<HttpClient> GetPrivateHttpClient()
        {
            var client = httpClientFactory.CreateClient("SystemApiClient");
            var stringToken = await localStorageService.GetToken();
            if (string.IsNullOrEmpty(stringToken)) return client;

            var deserialzeToken = Serializations.DeserializeJsonString<UserSession>(stringToken);
            if (deserialzeToken == null) return client;

            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", deserialzeToken.Token);
            return client;
        }

        public HttpClient GetPublicHttpClient()
        {
            var client = httpClientFactory.CreateClient("SystemApiClient");
            client.DefaultRequestHeaders.Remove(HeaderKey);
            return client;
        }
    }
}
