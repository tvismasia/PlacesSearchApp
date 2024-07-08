using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;


namespace SearchPlaceApp.Services
{
    //AuthService handles authentication and token management
    public class AuthService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public AuthService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }

        public async Task<string> GetAccessTokenAsync()
        {
            var clientId = _config["KerridgeApi:ClientId"];
            var clientSecret = _config["KerridgeApi:ClientSecret"];
            var tokenEndpoint = _config["KerridgeApi:IdentityBaseUrl"] + _config["KerridgeApi:TokenEndpoint"];
            var scope = _config["KerridgeApi:Scope"];

            var response = await _httpClient.PostAsync(tokenEndpoint, new FormUrlEncodedContent(new Dictionary<string, string>
        {
            { "client_id", clientId },
            { "client_secret", clientSecret },
            { "grant_type", "client_credentials" },
            { "scope", scope }
        }));

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Failed to get access token.");
            }

            var tokenResponse = await response.Content.ReadFromJsonAsync<TokenResponse>();
            return tokenResponse?.AccessToken;
        }

        private class TokenResponse
        {
            public string AccessToken { get; set; }
        }
    }
}
