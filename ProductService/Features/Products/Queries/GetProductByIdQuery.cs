using MediatR;
using ProductService.DTOs;

namespace ProductService.Features.Products.Queries
{
    public class GetProductByIdQuery : IRequest<ProductDto>
    {
        public int Id { get; set; }
    }
}
