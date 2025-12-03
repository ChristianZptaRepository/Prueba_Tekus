using AutoMapper;
using FluentValidation;
using Tekus.Application.DTOs;
using Tekus.Application.Interfaces;
using Tekus.Domain.Entities;
using Tekus.Domain.Interfaces;

namespace Tekus.Application.Features.Services
{
    public class CreateServiceHandler
    {
        private readonly IProviderRepository _providerRepository;
        private readonly IServiceRepository _serviceRepository;
        private readonly ICountryService _countryService;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateServiceRequest> _validator;

        public CreateServiceHandler(
            IProviderRepository providerRepository,
            IServiceRepository serviceRepository,
            ICountryService countryService,
            IMapper mapper,
            IValidator<CreateServiceRequest> validator)
        {
            _providerRepository = providerRepository;
            _serviceRepository = serviceRepository;
            _countryService = countryService;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<ServiceDto> Handle(CreateServiceRequest request)
        {
            var validationResult = await _validator.ValidateAsync(request);
            if (!validationResult.IsValid) throw new ValidationException(validationResult.Errors);

            var provider = await _providerRepository.GetByIdAsync(request.ProviderId);
            if (provider == null)
            {
                throw new KeyNotFoundException($"No se encontró el proveedor con ID {request.ProviderId}");
            }

            await _countryService.ValidateCountryCodesAsync(request.CountryCodes);

            var service = new Service(request.Name, request.HourlyRate, request.ProviderId);
            service.SetCountries(request.CountryCodes);

            provider.AddService(service);

            await _serviceRepository.AddAsync(service);

            return _mapper.Map<ServiceDto>(service);
        }
    }
}
