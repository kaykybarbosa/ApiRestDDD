using RestApiDDD.Application.DTOs;
using RestApiDDD.Domain.Entities;
using RestApiDDD.Infrastructure.CrossCutting.Intefaces;

namespace RestApiDDD.Infrastructure.CrossCutting.Mapper
{
    public class MapperProduc : IMapperProduct
    {
        public Product MapperEntityToDto(ProductDto productDto)
        {
            var product = new Product() 
            { 
                Id = productDto.Id,
                Name = productDto.Name,
                Price = productDto.Price,
            };
            
            return product;
        }

        public ProductDto MapperEntityToDto(Product product)
        {
            var productDto = new ProductDto()
            {
                Id= product.Id,
                Name = product.Name,
                Price = product.Price,
            };

            return productDto;
        }

        public IEnumerable<ProductDto> MapperListProductDto(IEnumerable<Product> products)
        {
            var productsDto = products.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price
            });

            return productsDto;
        }
    }
}