using AutoMapper;
using Tekus.Application.DTOs;
using Tekus.Domain.Entities;

namespace Tekus.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Provider, ProviderDto>();

            CreateMap<Service, ServiceDto>();
        }
    }
}
