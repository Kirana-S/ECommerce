namespace ECommerce.Api.Search.Interface
{
    public interface ICustomersService
    {
        Task<(bool IsSuccess, dynamic Customer, string ErrorMessage)> GetCustomerAsync(int id);
    }
}
