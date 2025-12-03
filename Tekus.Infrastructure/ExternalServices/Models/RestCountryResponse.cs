using System.Text.Json.Serialization;

namespace Tekus.Infrastructure.ExternalServices.Models
{
    public class RestCountryResponse
    {
        [JsonPropertyName("name")]
        public NameInfo Name { get; set; }
        [JsonPropertyName("cca2")]
        public string Cca2 { get; set; }
    }

    public class NameInfo
    {
        [JsonPropertyName("common")]
        public string Common { get; set; }
    }
}
