using Microsoft.EntityFrameworkCore;
using Tekus.Domain.Entities;
using Tekus.Infrastructure.Repositories;
using Xunit;
using Tekus.Infrastructure.Persistence;

namespace Tekus.Tests.Infrastructure.Repositories
{
    public class ProviderRepositoryTests
    {
        private readonly DbContextOptions<AppDbContext> _dbContextOptions;

        public ProviderRepositoryTests()
        {
            _dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: $"ProviderTestDb_{Guid.NewGuid()}")
                .Options;
        }

        private AppDbContext CreateContext()
        {
            return new AppDbContext(_dbContextOptions);
        }

        [Fact]
        public async Task UpdateAsync_UpdatesEntityAndSavesChanges()
        {
            var providerId = Guid.NewGuid();
            var initialName = "Initial Name";

            using (var context = CreateContext())
            {
                var provider = new Provider(providerId,initialName, "111-1", "initial@test.com",null);
                context.Providers.Add(provider);
                await context.SaveChangesAsync();
            }

            using (var context = CreateContext())
            {
                var repository = new ProviderRepository(context);

                var providerToUpdate = await repository.GetByIdAsync(providerId);

                providerToUpdate.UpdateDetails(
                    "Updated Name",
                    "222-2",
                    "updated@test.com",
                    new Dictionary<string, string> { { "new", "value" } }
                );
                await repository.UpdateAsync(providerToUpdate);
            }

            using (var context = CreateContext())
            {
                var updatedProvider = await context.Providers.FindAsync(providerId);

                Assert.NotNull(updatedProvider);
                Assert.Equal("Updated Name", updatedProvider.Name);
                Assert.Equal("222-2", updatedProvider.Nit);
                Assert.True(updatedProvider.CustomAttributes.ContainsKey("new"));
            }
        }
    }
}
