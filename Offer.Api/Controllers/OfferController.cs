using Common.Transversal;
using Common.Transversal.Events;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace Offer.Api.Controllers
{
    public class OfferController: BaseController<OfferController>
    {
        private readonly IPublishEndpoint publisher;

        public OfferController(IPublishEndpoint publisher)
        {
            this.publisher = publisher;
        }

        [HttpPost]
        public IActionResult AcceptedOrder(CancellationToken cancellationToken)
        {
            var newOffer = new OfferAcceptedEvent
            {
                OrderId = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                ProductName = "Product test 1",
                Quantity = 2
            };
            publisher.Publish<OfferAcceptedEvent>(newOffer, cancellationToken);

            return Ok(newOffer);
        }
    }
}
