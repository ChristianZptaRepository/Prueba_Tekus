using Tekus.Domain.Common;
using Tekus.Domain.Entities;

namespace Tekus.Domain.Interfaces
{
    public interface IServiceRepository
    {
        Task<int> CountAsync();
        Task<List<List<string>>> GetAllCountriesAsync();
        Task AddAsync(Service service);
        Task<PaginatedResponse<Service>> GetAllAsync(int pageNumber, int pageSize);
    }
}
