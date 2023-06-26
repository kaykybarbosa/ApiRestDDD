using RestApiDDD.Application.DTOs.Request;
using RestApiDDD.Application.DTOs.Response;
using RestApiDDD.Application.Interfaces.Mappers;
using RestApiDDD.Domain.Entities;

namespace RestApiDDD.Application.Mappers
{
    public class MapperProduct : IMapperProduct
    {
        public Product MapperDtoToEntity(ProductRequestDTO productDto)
        {
            var product = new Product()
            {
                Name = productDto.Name,
                Price = productDto.Price,
            };

            return product;
        }

        public ProductResponseDTO MapperEntityToDto(Product product)
        {
            var productDto = new ProductResponseDTO()
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                IsAvaiable = product.IsAvaiable
            };

            return productDto;
        }

        public IEnumerable<ProductResponseDTO> MapperListProductDto(IEnumerable<Product> products)
        {
            var productsDto = products.Select(p => new ProductResponseDTO
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                IsAvaiable = p.IsAvaiable
            });

            return productsDto;
        }
    }
}