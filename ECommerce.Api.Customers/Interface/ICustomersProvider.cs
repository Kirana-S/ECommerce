using ECommerce.Api.Customers.Model;

namespace ECommerce.Api.Customers.Interface;

public interface ICustomersProvider
{
    Task<(bool IsSuccess, IEnumerable<Customer> Customers, string ErrorMessage)> GetCustomersAsync();
    Task<(bool IsSuccess, Customer Customer, string ErrorMessage)> GetCustomerAsync(int id);
}