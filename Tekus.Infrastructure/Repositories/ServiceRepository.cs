using Microsoft.EntityFrameworkCore;
using Tekus.Domain.Common;
using Tekus.Domain.Entities;
using Tekus.Domain.Interfaces;
using Tekus.Infrastructure.Persistence;

namespace Tekus.Infrastructure.Repositories
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly AppDbContext _context;
        public ServiceRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<int> CountAsync()
        {
            return await _context.Services.CountAsync();
        }

        public async Task<List<List<string>>> GetAllCountriesAsync()
        {
            return await _context.Services.Select(s => s.Countries).ToListAsync();
        }
        public async Task AddAsync(Service service)
        {
            await _context.Services.AddAsync(service);
            await _context.SaveChangesAsync();
        }
        public async Task<PaginatedResponse<Service>> GetAllAsync(int pageNumber, int pageSize)
        {
            pageNumber = pageNumber <= 0 ? 1 : pageNumber;
            pageSize = pageSize <= 0 ? 10 : pageSize;

            var totalCount = await _context.Services.CountAsync();

            var skip = (pageNumber - 1) * pageSize;

            var services = await _context.Services
                .Include(s => s.Provider) 
                .Skip(skip)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();

            return PaginatedResponse<Service>.Create(services, pageNumber, pageSize, totalCount);
        }
        public async Task<Service?> GetByIdAsync(Guid id)
        {
            return await _context.Services
                .FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task UpdateAsync(Service service)
        {
            _context.Services.Update(service);
            await _context.SaveChangesAsync();
        }
    }
}
