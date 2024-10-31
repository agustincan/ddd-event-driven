using Common.Transversal;
using Common.Transversal.Commands.Orders;
using Common.Transversal.Events.Orders;
using Microsoft.AspNetCore.Mvc;
using Order.Saga.Api.Sagas.Orders;

namespace Order.Saga.Api.Controllers
{
    public class OrderSagaController : BaseController<OrderSagaController>
    {
        //private readonly OrderSagaOrchestrator sagaOrchestrator;
        private readonly IOrderSagaOrchestratorGpt sagaOrchestratorGpt;

        public OrderSagaController(IOrderSagaOrchestratorGpt sagaOrchestratorGpt)
        {
            this.sagaOrchestratorGpt = sagaOrchestratorGpt;
        }
        //public OrderSagaController(OrderSagaOrchestrator sagaOrchestrator, OrderSagaOrchestratorGpt sagaOrchestratorGpt)
        //{
        //    this.sagaOrchestrator = sagaOrchestrator;
        //    this.sagaOrchestratorGpt = sagaOrchestratorGpt;
        //}

        //[HttpPost("create-order")]
        //public async Task<IActionResult> CreateOrder([FromBody] OrderCreateCommand request)
        //{
        //    var saga = await sagaOrchestrator.ProcessOrderAsync(request.CustomerId, request.Items);
           
        //    return Ok(saga);
        //}

        [HttpPost("create-order-gpt")]
        public async Task<IActionResult> CreateOrderGpt([FromBody] OrderCreateCommand request)
        {
            var orderCreated = new OrderCreated(Guid.NewGuid());
            await sagaOrchestratorGpt.Handle(orderCreated);

            return Ok(orderCreated);
        }

    }
}

