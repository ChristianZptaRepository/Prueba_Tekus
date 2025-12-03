using Tekus.Domain.Common;

namespace Tekus.Domain.Entities
{
    public class Service : BaseEntity
    {
        public string Name { get; private set; }
        public decimal HourlyRate { get; private set; }
        public Guid ProviderId { get; private set; }
        public Provider Provider { get; private set; }
        public List<string> Countries { get; private set; } = new();

        protected Service() { }

        public Service(string name, decimal hourlyRate, Guid providerId)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("El nombre del servicio es obligatorio.");
            if (hourlyRate < 0) throw new ArgumentException("El valor por hora no puede ser negativo.");

            Name = name;
            HourlyRate = hourlyRate;
            ProviderId = providerId;
        }
        public void SetCountries(List<string> countries)
        {
            if (countries == null || !countries.Any())
                throw new ArgumentException("El servicio debe operar en al menos un país.");
            Countries = countries;
        }
    }
}
