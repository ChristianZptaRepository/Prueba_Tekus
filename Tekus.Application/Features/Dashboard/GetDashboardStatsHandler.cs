using Tekus.Application.DTOs;
using Tekus.Domain.Interfaces;

namespace Tekus.Application.Features.Dashboard
{
    public class GetDashboardStatsHandler
    {
        private readonly IProviderRepository _providerRepository;
        private readonly IServiceRepository _serviceRepository;
        public GetDashboardStatsHandler(
            IProviderRepository providerRepository,
            IServiceRepository serviceRepository)
        {
            _providerRepository = providerRepository;
            _serviceRepository = serviceRepository;
        }

        public async Task<DashboardDto> Handle()
        {
            var totalProviders = await _providerRepository.CountAsync();
            var totalServices = await _serviceRepository.CountAsync();

            var countriesLists = await _serviceRepository.GetAllCountriesAsync();

            var stats = countriesLists
                .SelectMany(c => c)
                .GroupBy(code => code)
                .Select(g => new CountryStatDto
                {
                    CountryCode = g.Key,
                    CountryName = g.Key,
                    Count = g.Count()
                })
                .OrderByDescending(x => x.Count)
                .ToList();

            return new DashboardDto
            {
                TotalProviders = totalProviders,
                TotalServices = totalServices,
                ServicesPerCountry = stats
            };
        }
    }
}
