using RestApiDDD.Application.DTOs;
using RestApiDDD.Domain.Entities;

namespace RestApiDDD.Application.Interfaces.Mappers
{
    public interface IMapperProduct
    {
        Product MapperDtoToEntity(ProductDto productDto);
        IEnumerable<ProductDto> MapperListProductDto(IEnumerable<Product> products);
        ProductDto MapperEntityToDto(Product product);
    }
}