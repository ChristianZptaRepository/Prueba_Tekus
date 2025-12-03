namespace Tekus.Domain.Common
{
    public class PaginatedResponse<T>
    {
        public IReadOnlyList<T> Data { get; }
        public int PageNumber { get; }
        public int PageSize { get; }
        public int TotalCount { get; }
        public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
        private PaginatedResponse(IReadOnlyList<T> data, int pageNumber, int pageSize, int totalCount)
        {
            Data = data;
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalCount = totalCount;
        }
        public static PaginatedResponse<T> Create(IReadOnlyList<T> data, int pageNumber, int pageSize, int totalCount)
        {
            return new PaginatedResponse<T>(data, pageNumber, pageSize, totalCount);
        }
    }
}
