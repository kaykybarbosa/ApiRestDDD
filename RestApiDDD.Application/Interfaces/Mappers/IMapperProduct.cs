using RestApiDDD.Application.DTOs.Request;
using RestApiDDD.Application.DTOs.Response;
using RestApiDDD.Domain.Entities;

namespace RestApiDDD.Application.Interfaces.Mappers
{
    public interface IMapperProduct
    {
        Product MapperDtoToEntity(ProductRequestDTO productDto);
        IEnumerable<ProductResponseDTO> MapperListProductDto(IEnumerable<Product> products);
        ProductResponseDTO MapperEntityToDto(Product product);
    }
}