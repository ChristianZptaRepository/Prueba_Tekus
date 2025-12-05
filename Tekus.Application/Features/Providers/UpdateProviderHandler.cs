using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekus.Domain.Interfaces;

namespace Tekus.Application.Features.Providers
{
    public class UpdateProviderHandler : IRequestHandler<UpdateProviderCommand, Unit>
    {
        private readonly IProviderRepository _providerRepository;

        public UpdateProviderHandler(IProviderRepository providerRepository)
        {
            _providerRepository = providerRepository;
        }

        public async Task<Unit> Handle(UpdateProviderCommand request, CancellationToken cancellationToken)
        {
            var provider = await _providerRepository.GetByIdAsync(request.Id);

            if (provider == null)
            {
                throw new KeyNotFoundException($"El proveedor con ID {request.Id} no existe.");
            }

            provider.UpdateDetails(
                request.Name,
                request.Nit,
                request.Email,
                request.CustomAttributes
            );

            await _providerRepository.UpdateAsync(provider);

            return Unit.Value;
        }
    }
}
