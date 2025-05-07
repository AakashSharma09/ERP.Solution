using ERP.Shared.Events;
using MassTransit;

namespace InventoryService.Consumer
{
    public class ProductCreatedEventConsumer : IConsumer<ProductCreatedEvent>
    {
        public async Task Consume(ConsumeContext<ProductCreatedEvent> context)
        {
            var product = context.Message;

            // Example: Create stock record
            Console.WriteLine($"[InventoryService] New product added: {product.Name}, creating inventory...");

            // TODO: Add inventory creation logic here

            await Task.CompletedTask;
        }
    }
}
