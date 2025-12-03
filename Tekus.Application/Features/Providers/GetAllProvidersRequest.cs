namespace Tekus.Application.Features.Providers
{
    public class GetAllProvidersRequest
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
