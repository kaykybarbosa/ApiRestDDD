using RestApiDDD.Application.DTOs.Request;
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

        public ProductRequestDTO MapperEntityToDto(Product product)
        {
            var productDto = new ProductRequestDTO()
            {
                Name = product.Name,
                Price = product.Price,
            };

            return productDto;
        }

        public IEnumerable<ProductRequestDTO> MapperListProductDto(IEnumerable<Product> products)
        {
            var productsDto = products.Select(p => new ProductRequestDTO
            {
                Name = p.Name,
                Price = p.Price
            });

            return productsDto;
        }
    }
}