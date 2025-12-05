using Moq;
using Tekus.Application.Features.Providers;
using Tekus.Domain.Entities;
using Tekus.Domain.Interfaces;
using Xunit;

namespace Tekus.Tests.Application.Providers
{
    public class UpdateProviderHandlerTests
    {
        private readonly Mock<IProviderRepository> _mockRepo;
        private readonly UpdateProviderHandler _handler;

        public UpdateProviderHandlerTests()
        {
            _mockRepo = new Mock<IProviderRepository>();
            _handler = new UpdateProviderHandler(_mockRepo.Object);
        }

        [Fact]
        public async Task Handle_ValidCommand_UpdatesProviderAndCallsRepository()
        {
            var providerId = Guid.NewGuid();
            var existingProvider = new Provider("Old Name", "123-4", "old@test.com");

            var command = new UpdateProviderCommand
            {
                Id = providerId,
                Name = "New Updated Name",
                Nit = "987-6",
                Email = "new@updated.com",
                CustomAttributes = new Dictionary<string, string> { { "key", "value" } }
            };

            _mockRepo.Setup(r => r.GetByIdAsync(providerId))
                     .ReturnsAsync(existingProvider);

            await _handler.Handle(command, CancellationToken.None);

            Assert.Equal("New Updated Name", existingProvider.Name);
            Assert.Equal("new@updated.com", existingProvider.Email);

            _mockRepo.Verify(r => r.UpdateAsync(It.IsAny<Provider>()), Times.Once);
        }

        [Fact]
        public async Task Handle_NonExistingProvider_ThrowsKeyNotFoundException()
        {
            var providerId = Guid.NewGuid();
            var command = new UpdateProviderCommand { Id = providerId, Name = "Test" };

            _mockRepo.Setup(r => r.GetByIdAsync(providerId)).ReturnsAsync((Provider)null!);

            await Assert.ThrowsAsync<KeyNotFoundException>(
                () => _handler.Handle(command, CancellationToken.None)
            );
            _mockRepo.Verify(r => r.UpdateAsync(It.IsAny<Provider>()), Times.Never);
        }
    }
}
