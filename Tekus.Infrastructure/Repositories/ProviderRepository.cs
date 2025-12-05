using Microsoft.EntityFrameworkCore;
using Tekus.Domain.Common;
using Tekus.Domain.Entities;
using Tekus.Domain.Interfaces;
using Tekus.Infrastructure.Persistence;

namespace Tekus.Infrastructure.Repositories
{
    public class ProviderRepository : IProviderRepository
    {
        private readonly AppDbContext _context;

        public ProviderRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Provider provider)
        {
            await _context.Providers.AddAsync(provider);
            await _context.SaveChangesAsync();
        }

        public async Task<Provider?> GetByIdAsync(Guid id)
        {
            return await _context.Providers
                .Include(p => p.Services)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<bool> ExistsWithNitAsync(string nit)
        {
            return await _context.Providers.AnyAsync(p => p.Nit == nit);
        }
        public async Task<PaginatedResponse<Provider>> GetAllAsync(int pageNumber, int pageSize)
        {
            pageNumber = pageNumber <= 0 ? 1 : pageNumber;
            pageSize = pageSize <= 0 ? 10 : pageSize;

            var totalCount = await _context.Providers.CountAsync();

            var skip = (pageNumber - 1) * pageSize;

            var providers = await _context.Providers
                .Skip(skip)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();

            return PaginatedResponse<Provider>.Create(providers, pageNumber, pageSize, totalCount);
        }
        public async Task<int> CountAsync()
        {
            return await _context.Providers.CountAsync();
        }
        public async Task UpdateAsync(Provider provider)
        {
            _context.Providers.Update(provider);
            await _context.SaveChangesAsync();
        }
    }
}
