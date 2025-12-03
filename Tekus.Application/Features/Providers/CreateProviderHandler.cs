using AutoMapper;
using FluentValidation;
using Tekus.Application.DTOs;
using Tekus.Domain.Entities;
using Tekus.Domain.Interfaces;

namespace Tekus.Application.Features.Providers
{
    public class CreateProviderHandler
    {
        private readonly IProviderRepository _repository;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateProviderRequest> _validator;

        public CreateProviderHandler(
            IProviderRepository repository,
            IMapper mapper,
            IValidator<CreateProviderRequest> validator)
        {
            _repository = repository;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<ProviderDto> Handle(CreateProviderRequest request)
        {
            var validationResult = await _validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            if (await _repository.ExistsWithNitAsync(request.Nit))
            {
                throw new InvalidOperationException($"Ya existe un proveedor con el NIT {request.Nit}");
            }

            var provider = new Provider(request.Nit, request.Name, request.Email);

            if (request.CustomAttributes != null)
            {
                foreach (var item in request.CustomAttributes)
                {
                    provider.AddOrUpdateCustomField(item.Key, item.Value);
                }
            }

            await _repository.AddAsync(provider);

            return _mapper.Map<ProviderDto>(provider);
        }
    }
}
