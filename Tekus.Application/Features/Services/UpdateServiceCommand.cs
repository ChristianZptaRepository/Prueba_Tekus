using MediatR;

namespace Tekus.Application.Features.Services
{
    public class UpdateServiceCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal HourlyRate { get; set; }
        public Guid ProviderId { get; set; }
        public List<string> CountryCodes { get; set; } = new();
    }
}
