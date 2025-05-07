using ERP.Core.Entities;
using ERP.Infrastructure.Data;
using ERP.Shared.Events;
using MassTransit;
using MediatR;

namespace ProductService.Features.Products.Commands
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
    {
        private readonly ProductDbContext _context;
        private readonly IPublishEndpoint _publishEndpoint;

        public CreateProductCommandHandler(ProductDbContext context, IPublishEndpoint publishEndpoint)
        {
            _context = context;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                Name = request.Name,
                Price = request.Price
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync(cancellationToken);

            // Publish event to RabbitMQ
            await _publishEndpoint.Publish(new ProductCreatedEvent
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price
            });

            return product.Id;
        }
    }
}
