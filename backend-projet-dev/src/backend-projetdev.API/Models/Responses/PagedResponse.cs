namespace backend_projetdev.API.Models.Responses
{
    public class PagedResponse<T>
    {
        public IEnumerable<T> Data { get; set; }
        public int TotalCount { get; set; }

        public PagedResponse(IEnumerable<T> data, int totalCount)
        {
            Data = data;
            TotalCount = totalCount;
        }
    }
}