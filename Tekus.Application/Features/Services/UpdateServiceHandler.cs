using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekus.Application.Interfaces;
using Tekus.Domain.Interfaces;

namespace Tekus.Application.Features.Services
{
    public class UpdateServiceHandler : IRequestHandler<UpdateServiceCommand, Unit>
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly ICountryService _countryService;

        public UpdateServiceHandler(IServiceRepository serviceRepository, ICountryService countryService)
        {
            _serviceRepository = serviceRepository;
            _countryService = countryService;
        }

        public async Task<Unit> Handle(UpdateServiceCommand request, CancellationToken cancellationToken)
        {
            var service = await _serviceRepository.GetByIdAsync(request.Id);
            if (service == null)
                throw new KeyNotFoundException($"El servicio con ID {request.Id} no existe.");

            if (request.CountryCodes != null && request.CountryCodes.Any())
            {
                await _countryService.ValidateCountryCodesAsync(request.CountryCodes);
            }
            service.UpdateDetails(
                request.Name,
                request.HourlyRate,
                request.ProviderId,
                request.CountryCodes
            );

            await _serviceRepository.UpdateAsync(service);

            return Unit.Value;
        }
    }
}
