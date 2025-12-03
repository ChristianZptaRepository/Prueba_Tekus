using FluentValidation;
using Tekus.Application.DTOs;

namespace Tekus.Application.Features.Services
{
    public class CreateServiceValidator : AbstractValidator<CreateServiceRequest>
    {
        public CreateServiceValidator()
        {
            RuleFor(s => s.ProviderId)
                .NotEmpty().WithMessage("El ID del proveedor es obligatorio.");

            RuleFor(s => s.Name)
                .NotEmpty().WithMessage("El nombre del servicio es obligatorio.");

            RuleFor(s => s.HourlyRate)
                .GreaterThan(0).WithMessage("El valor por hora debe ser mayor a 0.");

            RuleFor(s => s.CountryCodes)
                .NotEmpty().WithMessage("Debe especificar al menos un país.")
                .Must(c => c.Count > 0).WithMessage("La lista de países no puede estar vacía.");
        }
    }
}
