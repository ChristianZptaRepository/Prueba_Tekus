using Microsoft.Extensions.Caching.Memory;
using System.Net.Http.Json;
using Tekus.Application.DTOs;
using Tekus.Application.Interfaces;
using Tekus.Infrastructure.ExternalServices.Models;

namespace Tekus.Infrastructure.ExternalServices
{
    public class CountryService : ICountryService
    {
        private readonly HttpClient _httpClient;
        private readonly IMemoryCache _cache;

        private const string CacheKey = "Countries_List";

        public CountryService(HttpClient httpClient, IMemoryCache cache)
        {
            _httpClient = httpClient;
            _cache = cache;
        }

        public async Task<List<CountryDto>> GetCountriesAsync()
        {
            if (_cache.TryGetValue(CacheKey, out List<CountryDto>? cachedCountries))
            {
                return cachedCountries!;
            }

            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<RestCountryResponse>>(
                    "https://restcountries.com/v3.1/all?fields=name,cca2");

                if (response == null) return new List<CountryDto>();

                var countries = response
                    .Select(c => new CountryDto
                    {
                        Name = c.Name.Common,
                        Code = c.Cca2
                    })
                    .OrderBy(c => c.Name)
                    .ToList();

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromHours(1));

                _cache.Set(CacheKey, countries, cacheOptions);

                return countries;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching countries: {ex.Message}");
                return new List<CountryDto>();
            }
        }

        public async Task<string?> GetCountryNameByCodeAsync(string code)
        {
            var countries = await GetCountriesAsync();
            var country = countries.FirstOrDefault(c => c.Code.Equals(code, StringComparison.OrdinalIgnoreCase));
            return country?.Name;
        }
    }
}
