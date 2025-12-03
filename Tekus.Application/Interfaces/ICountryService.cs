using Tekus.Application.DTOs;

namespace Tekus.Application.Interfaces
{
    public interface ICountryService
    {
        Task<List<CountryDto>> GetCountriesAsync();
        Task<string?> GetCountryNameByCodeAsync(string code);
        Task ValidateCountryCodesAsync(List<string> codes);
    }
}
