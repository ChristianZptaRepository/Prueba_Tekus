using AutoMapper;
using FluentValidation;
using Tekus.Application.DTOs;
using Tekus.Domain.Interfaces;

namespace Tekus.Application.Features.Providers
{
    public class GetAllProvidersHandler
    {
        private readonly IProviderRepository _repository;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateProviderRequest> _validator;

        public GetAllProvidersHandler(
            IProviderRepository repository,
            IMapper mapper,
            IValidator<CreateProviderRequest> validator)
        {
            _repository = repository;
            _mapper = mapper;
            _validator = validator;
        }
        public async Task<PaginatedListDto<ProviderDto>> Handle(GetAllProvidersRequest request)
        {
            var paginatedProviders = await _repository.GetAllAsync(
                request.PageNumber,
                request.PageSize
            );

            var providerDtos = _mapper.Map<IReadOnlyList<ProviderDto>>(paginatedProviders.Data);

            return new PaginatedListDto<ProviderDto>
            {
                Items = providerDtos,
                PageNumber = paginatedProviders.PageNumber,
                PageSize = paginatedProviders.PageSize,
                TotalCount = paginatedProviders.TotalCount,
                TotalPages = paginatedProviders.TotalPages
            };
        }
    }
}
