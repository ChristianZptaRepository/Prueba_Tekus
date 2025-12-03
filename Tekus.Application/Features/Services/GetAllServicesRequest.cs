namespace Tekus.Application.Features.Services
{
    public class GetAllServicesRequest
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
