using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekus.Domain.Common;
using Tekus.Domain.Entities;

namespace Tekus.Domain.Interfaces
{
    public interface IProviderRepository
    {
        Task<Provider?> GetByIdAsync(Guid id);
        Task AddAsync(Provider provider);
        Task UpdateAsync(Provider provider);
        Task<bool> ExistsWithNitAsync(string nit);
        Task<int> CountAsync();
        Task<PaginatedResponse<Provider>> GetAllAsync(int pageNumber, int pageSize);
    }
}
