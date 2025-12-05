namespace Tekus.Application.DTOs
{
    public class ServiceDto
    {        
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal HourlyRate { get; set; }
        public Guid ProviderId { get; set; }
        public List<string> CountryCodes { get; set; } = new();
    }
    public class CreateServiceRequest
    {
        public Guid ProviderId { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal HourlyRate { get; set; }
        public List<string> CountryCodes { get; set; } = new();
    }
}
