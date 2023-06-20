using RestApiDDD.Application.DTOs;
using RestApiDDD.Application.Interfaces;
using RestApiDDD.Application.Interfaces.Mappers;
using RestApiDDD.Domain.Core.Interfaces.Services;

namespace RestApiDDD.Application
{
    public class ApplicationServiceProduct : IApplicationSerivceProduct
    {
        private readonly IServiceProduct _serviceProduct;
        private readonly IMapperProduct _mapperProduct;

        public ApplicationServiceProduct(IServiceProduct serviceProduct,
                                         IMapperProduct mapperProduct)
        {
            _serviceProduct = serviceProduct;
            _mapperProduct = mapperProduct;
        }
        public void Add(ProductDto productDto)
        {
            var product = _mapperProduct.MapperDtoToEntity(productDto);
            _serviceProduct.Add(product);
        }

        public void Delete(ProductDto productDto)
        {
            var product = _mapperProduct.MapperDtoToEntity(productDto);
            _serviceProduct.Delete(product);
        }

        public IEnumerable<ProductDto> GetAll()
        {
            var products = _serviceProduct.GetAll();
            return _mapperProduct.MapperListProductDto(products);
        }

        public ProductDto GetById(Guid id)
        {
            var product = _serviceProduct.GetById(id);
            return _mapperProduct.MapperEntityToDto(product);
        }

        public void Update(ProductDto productDto)
        {
            var product = _mapperProduct.MapperDtoToEntity(productDto);
            _serviceProduct.Update(product);
        }
    }
}