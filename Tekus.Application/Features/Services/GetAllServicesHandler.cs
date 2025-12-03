using AutoMapper;
using Tekus.Application.DTOs;
using Tekus.Domain.Interfaces;

namespace Tekus.Application.Features.Services
{
    public class GetAllServicesHandler
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IMapper _mapper;

        public GetAllServicesHandler(IServiceRepository serviceRepository, IMapper mapper)
        {
            _serviceRepository = serviceRepository;
            _mapper = mapper;
        }

        public async Task<PaginatedListDto<ServiceDto>> Handle(GetAllServicesRequest request)
        {
            var paginatedServices = await _serviceRepository.GetAllAsync(
                request.PageNumber,
                request.PageSize
            );

            var serviceDtos = _mapper.Map<IReadOnlyList<ServiceDto>>(paginatedServices.Data);

            return new PaginatedListDto<ServiceDto>
            {
                Items = serviceDtos,
                PageNumber = paginatedServices.PageNumber,
                PageSize = paginatedServices.PageSize,
                TotalCount = paginatedServices.TotalCount,
                TotalPages = paginatedServices.TotalPages
            };
        }
    }
}
