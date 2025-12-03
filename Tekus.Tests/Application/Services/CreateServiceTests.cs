using Xunit;
using Moq;
using System;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Tekus.Application.DTOs;
using Tekus.Application.Features.Services;
using Tekus.Application.Interfaces;
using Tekus.Domain.Interfaces;
using Tekus.Domain.Entities;

namespace Tekus.Tests.Application.Services
{
    public class CreateServiceTests
    {
        private readonly Mock<IProviderRepository> _mockProviderRepo;
        private readonly Mock<IServiceRepository> _mockServiceRepo;
        private readonly Mock<ICountryService> _mockCountryService;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IValidator<CreateServiceRequest>> _mockValidator;
        private readonly CreateServiceHandler _handler;
        private readonly Guid _validProviderId = Guid.NewGuid();

        public CreateServiceTests()
        {
            _mockProviderRepo = new Mock<IProviderRepository>();
            _mockServiceRepo = new Mock<IServiceRepository>();
            _mockCountryService = new Mock<ICountryService>();
            _mockMapper = new Mock<IMapper>();
            _mockValidator = new Mock<IValidator<CreateServiceRequest>>();
        
            _mockValidator.Setup(v => v.ValidateAsync(It.IsAny<CreateServiceRequest>(), default))
                          .ReturnsAsync(new ValidationResult());

            _mockProviderRepo.Setup(r => r.GetByIdAsync(_validProviderId))
                             .ReturnsAsync(new Provider("999-9", "Test Provider", "test@test.co"));

            _handler = new CreateServiceHandler(
                _mockProviderRepo.Object,
                _mockServiceRepo.Object,
                _mockCountryService.Object,
                _mockMapper.Object,
                _mockValidator.Object
            );
        }

        [Fact]
        public async Task Handler_ShouldAddService_WhenProviderExistsAndCountriesAreValid()
        {
            var request = new CreateServiceRequest
            {
                ProviderId = _validProviderId,
                Name = "Test Service",
                HourlyRate = 100m,
                CountryCodes = new List<string> { "CO", "MX" }
            };

            _mockCountryService.Setup(c => c.ValidateCountryCodesAsync(request.CountryCodes)).Returns(Task.CompletedTask);

            await _handler.Handle(request);

            _mockServiceRepo.Verify(r => r.AddAsync(It.IsAny<Service>()), Times.Once);

            _mockCountryService.Verify(c => c.ValidateCountryCodesAsync(request.CountryCodes), Times.Once);
        }

        [Fact]
        public async Task Handler_ShouldThrowException_WhenProviderDoesNotExist()
        {
            var invalidId = Guid.NewGuid();
            var request = new CreateServiceRequest
            {
                ProviderId = invalidId,
                Name = "Test",
                HourlyRate = 10m,
                CountryCodes = new List<string> { "CO" }
            };

            _mockProviderRepo.Setup(r => r.GetByIdAsync(invalidId)).ReturnsAsync((Provider)null);

            await Assert.ThrowsAsync<KeyNotFoundException>(() => _handler.Handle(request));

            _mockServiceRepo.Verify(r => r.AddAsync(It.IsAny<Service>()), Times.Never);
        }

        [Fact]
        public async Task Handler_ShouldNotAddService_WhenCountryValidationFails()
        {
            var request = new CreateServiceRequest
            {
                ProviderId = _validProviderId,
                Name = "Test Service",
                HourlyRate = 100m,
                CountryCodes = new List<string> { "CO", "INVALID_CODE" }
            };

            _mockCountryService.Setup(c => c.ValidateCountryCodesAsync(request.CountryCodes))
                               .ThrowsAsync(new ArgumentException("El código 'INVALID_CODE' no es válido."));

            await Assert.ThrowsAsync<ArgumentException>(() => _handler.Handle(request));

            _mockServiceRepo.Verify(r => r.AddAsync(It.IsAny<Service>()), Times.Never);
        }
    }
}
