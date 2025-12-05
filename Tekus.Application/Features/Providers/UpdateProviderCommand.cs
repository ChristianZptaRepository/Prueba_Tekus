using MediatR;

namespace Tekus.Application.Features.Providers
{
    public class UpdateProviderCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Nit { get; set; }
        public string Email { get; set; }
        public Dictionary<string, string> CustomAttributes { get; set; } = new();
    }
}
