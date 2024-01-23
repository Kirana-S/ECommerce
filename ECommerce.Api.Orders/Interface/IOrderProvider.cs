namespace ECommerce.Api.Orders.Interface
{
    public interface IOrderProvider
    {
        Task<(bool IsSuccess, IEnumerable<Models.Order> Orders, string ErrorMessage)> GetOrdersAsync(int customerId);
    }
}
