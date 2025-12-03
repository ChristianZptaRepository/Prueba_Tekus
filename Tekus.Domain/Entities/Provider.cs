using Tekus.Domain.Common;

namespace Tekus.Domain.Entities
{
    public class Provider : BaseEntity
    {
        public string Nit { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        private readonly List<Service> _services = new();
        public IReadOnlyCollection<Service> Services => _services.AsReadOnly();
        public Dictionary<string, string> CustomAttributes { get; private set; } = new();
        protected Provider() { }

        public Provider(string nit, string name, string email)
        {
            if (string.IsNullOrWhiteSpace(nit)) throw new ArgumentException("El NIT es obligatorio.");
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("El nombre es obligatorio.");
            if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("El email es obligatorio.");

            Nit = nit;
            Name = name;
            Email = email;
        }
        public void AddService(Service service)
        {
            _services.Add(service);
        }
        public void AddOrUpdateCustomField(string fieldName, string value)
        {
            if (string.IsNullOrWhiteSpace(fieldName)) return;

            if (CustomAttributes.ContainsKey(fieldName))
            {
                CustomAttributes[fieldName] = value;
            }
            else
            {
                CustomAttributes.Add(fieldName, value);
            }
        }
    }
}
