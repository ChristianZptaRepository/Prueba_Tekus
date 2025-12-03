namespace Tekus.Application.DTOs
{
    public class DashboardDto
    {
        public int TotalProviders { get; set; }
        public int TotalServices { get; set; }
        public List<CountryStatDto> ServicesPerCountry { get; set; } = new();
    }

    public class CountryStatDto
    {
        public string CountryCode { get; set; } = string.Empty;
        public string CountryName { get; set; } = string.Empty;
        public int Count { get; set; }
    }
}
