namespace ECommerce.Api.Search.Interface
{
    public interface ISearchService
    {
        Task<(bool IsSuccess, dynamic SearchResults)> SearchAsync(int customerId);
    }
}
