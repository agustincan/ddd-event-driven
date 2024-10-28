﻿using Common.Transversal.Events;
using MassTransit;

namespace Order.Api.Consumers
{
    public class OfferDeletedConsumer : IConsumer<OfferDeletedEvent>
    {
        private readonly ILogger<OfferDeletedConsumer> logger;

        public OfferDeletedConsumer(ILogger<OfferDeletedConsumer> logger)
        {
            this.logger = logger;
        }
        public Task Consume(ConsumeContext<OfferDeletedEvent> context)
        {
            var message = context.Message;
            logger.LogInformation($"Offer Deleted {message.OrderId} time {message.CreatedAt}");
            
            return Task.CompletedTask;
        }
    }
}
