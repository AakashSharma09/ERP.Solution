using MediatR;

namespace ProductService.Features.Products.Commands
{
    public class CreateProductCommand : IRequest<int>
    {
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
    }
}

