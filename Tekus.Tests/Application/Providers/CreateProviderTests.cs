using Xunit;
using Moq;
using System;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using Tekus.Application.DTOs;
using Tekus.Application.Features.Providers;
using Tekus.Domain.Interfaces;
using Tekus.Domain.Entities;
using AutoMapper;

namespace Tekus.Tests.Application.Providers
{
    public class CreateProviderTests
    {
        private readonly Mock<IProviderRepository> _mockRepo;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IValidator<CreateProviderRequest>> _mockValidator;
        private readonly CreateProviderHandler _handler;

        public CreateProviderTests()
        {
            _mockRepo = new Mock<IProviderRepository>();
            _mockMapper = new Mock<IMapper>();
            _mockValidator = new Mock<IValidator<CreateProviderRequest>>();

            _mockValidator.Setup(v => v.ValidateAsync(It.IsAny<CreateProviderRequest>(), default))
                          .ReturnsAsync(new ValidationResult());

            _handler = new CreateProviderHandler(_mockRepo.Object, _mockMapper.Object, _mockValidator.Object);
        }

        [Fact]
        public async Task Handler_ShouldCallRepositoryAdd_WhenNitIsUnique()
        {
            var request = new CreateProviderRequest { Nit = "999-1", Name = "Test", Email = "t@t.co" };

            _mockRepo.Setup(r => r.ExistsWithNitAsync(request.Nit)).ReturnsAsync(false);

            await _handler.Handle(request);

            _mockRepo.Verify(r => r.AddAsync(It.IsAny<Provider>()), Times.Once);
        }

        [Fact]
        public async Task Handler_ShouldThrowException_WhenNitAlreadyExists()
        {
            var request = new CreateProviderRequest { Nit = "888-2", Name = "Dupe", Email = "d@d.co" };

            _mockRepo.Setup(r => r.ExistsWithNitAsync(request.Nit)).ReturnsAsync(true);

            await Assert.ThrowsAsync<InvalidOperationException>(() => _handler.Handle(request));

            _mockRepo.Verify(r => r.AddAsync(It.IsAny<Provider>()), Times.Never);
        }
    }
}
