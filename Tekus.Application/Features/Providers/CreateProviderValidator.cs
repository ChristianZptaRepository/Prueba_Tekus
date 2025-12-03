using FluentValidation;
using Tekus.Application.DTOs;

namespace Tekus.Application.Features.Providers
{
    public class CreateProviderValidator : AbstractValidator<CreateProviderRequest>
    {
        public CreateProviderValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("El nombre es obligatorio.")
                .MaximumLength(150).WithMessage("El nombre no puede exceder 150 caracteres.");

            RuleFor(p => p.Nit)
                .NotEmpty().WithMessage("El NIT es obligatorio.")
                .Matches(@"^\d+-\d$").WithMessage("El NIT debe tener formato numérico con guion (ej: 900123456-1).");

            RuleFor(p => p.Email)
                .NotEmpty().WithMessage("El correo es obligatorio.")
                .EmailAddress().WithMessage("El formato del correo no es válido.");
        }
    }
}
