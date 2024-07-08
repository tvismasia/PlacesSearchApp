using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace SearchPlaceApp.Services
{
    //Service to interact with the Api
    public class PlaceService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthService _authService;
        private readonly IConfiguration _config;

        public PlaceService(HttpClient httpClient, AuthService authService, IConfiguration config)
        {
            _httpClient = httpClient;
            _authService = authService;
            _config = config;
        }

        public async Task<List<Place>> SearchPlacesAsync(string criteria)
        {
            var accessToken = await _authService.GetAccessTokenAsync();
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

            var response = await _httpClient.GetAsync($"/api/v1/locations/places?criteria={criteria}");
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<List<Place>>();
        }

        public async Task<PlaceDetail> GetPlaceDetailAsync(string id)
        {
            var accessToken = await _authService.GetAccessTokenAsync();
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

            var response = await _httpClient.GetAsync($"/api/v1/locations/places/{id}");
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<PlaceDetail>();
        }
    }
}
