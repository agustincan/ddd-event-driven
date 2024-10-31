using Common.Transversal;
using Common.Transversal.Commands.Orders;
using Microsoft.AspNetCore.Mvc;

namespace Order.Api.Controllers.v1
{
    public class OrderController : BaseController<OrderController>
    {
        public OrderController()
        {
            
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] OrderCreateCommand request)
        {

            return Ok(request);
        }
    }
}
