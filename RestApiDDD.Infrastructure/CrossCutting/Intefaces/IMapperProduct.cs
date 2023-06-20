using RestApiDDD.Application.DTOs;
using RestApiDDD.Domain.Entities;

namespace RestApiDDD.Infrastructure.CrossCutting.Intefaces
{
    public interface IMapperProduct
    {
        Product MapperEntityToDto(ProductDto productDto);
        IEnumerable<ProductDto> MapperListProductDto(IEnumerable<Product> products);
        ProductDto MapperEntityToDto(Product product); 
    }
}