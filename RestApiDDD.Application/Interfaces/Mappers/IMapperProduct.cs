using RestApiDDD.Application.DTOs.Request;
using RestApiDDD.Domain.Entities;

namespace RestApiDDD.Application.Interfaces.Mappers
{
    public interface IMapperProduct
    {
        Product MapperDtoToEntity(ProductRequestDTO productDto);
        IEnumerable<ProductRequestDTO> MapperListProductDto(IEnumerable<Product> products);
        ProductRequestDTO MapperEntityToDto(Product product);
    }
}