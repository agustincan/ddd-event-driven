using Common.Transversal;
using Common.Transversal.Events.Offers;
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
        public IActionResult CreateOrder(CancellationToken cancellationToken)
        {
            var newOffer = new OfferAcceptedEvent
            {
                OrderId = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                ProductName = "Product test 1",
                Quantity = 2
            };
            publisher.Publish<OfferCreatedEvent>(newOffer, cancellationToken);

            return Ok(newOffer);
        }

        [HttpPost]
        public IActionResult AcceptOrder(CancellationToken cancellationToken)
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

        [HttpPut]
        public IActionResult UpdateOrder(CancellationToken cancellationToken)
        {
            var newOffer = new OfferUpdatedEvent
            {
                OrderId = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                ProductName = "Product updated 123",
                Quantity = 4
            };
            publisher.Publish<OfferUpdatedEvent>(newOffer, cancellationToken);

            return Ok(newOffer);
        }

        [HttpPatch]
        public IActionResult PatchOrder(CancellationToken cancellationToken)
        {
            var newOffer = new OfferPatchedEvent
            {
                OrderId = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                ProductName = "Product test 1",
                Quantity = 2
            };
            publisher.Publish<OfferPatchedEvent>(newOffer, cancellationToken);

            return Ok(newOffer);
        }

        [HttpDelete]
        public IActionResult DeleteOrder(CancellationToken cancellationToken)
        {
            var newOffer = new OfferDeletedEvent
            {
                OrderId = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                ProductName = "Product test 1",
                Quantity = 2
            };
            publisher.Publish<OfferDeletedEvent>(newOffer, cancellationToken);

            return Ok(newOffer);
        }
    }
}
