using Tekus.Domain.Entities;
using Xunit;

namespace Tekus.Tests.Domain
{
    public class ServiceTests
    {
        [Fact]
        public void Service_Constructor_ShouldThrowException_WhenHourlyRateIsNegative()
        {
            var providerId = Guid.NewGuid();

            Assert.Throws<ArgumentException>(() =>
                new Service("Servicio de prueba", -10m, providerId));
        }

        [Fact]
        public void Service_SetCountries_ShouldThrowException_WhenCountriesListIsEmpty()
        {
            var providerId = Guid.NewGuid();
            var service = new Service("Test", 100m, providerId);

            Assert.Throws<ArgumentException>(() =>
                service.SetCountries(new List<string>()));
        }
    }
}
