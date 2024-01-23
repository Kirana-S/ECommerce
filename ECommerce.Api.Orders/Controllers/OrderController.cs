using ECommerce.Api.Orders.Interface;
using ECommerce.Api.Orders.Providers;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Orders.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderProvider orderProvider;

        public OrderController(IOrderProvider orderProvider)
        {
            this.orderProvider = orderProvider;
        }

        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetOrdersAsync(int customerId)
        {
            var result = await orderProvider.GetOrdersAsync(customerId);
            if (result.IsSuccess)
            {
                return Ok(result.Orders);
            }
            return NotFound();
        }
    }
}
