using RestApiDDD.Application.DTOs.Request;
using RestApiDDD.Application.Interfaces;
using RestApiDDD.Application.Interfaces.Mappers;
using RestApiDDD.Domain.Core.Interfaces.Services;

namespace RestApiDDD.Application
{
    public class ApplicationServiceProduct : IApplicationServiceProduct
    {
        private readonly IServiceProduct _serviceProduct;
        private readonly IMapperProduct _mapperProduct;

        public ApplicationServiceProduct(IServiceProduct serviceProduct,
                                         IMapperProduct mapperProduct)
        {
            _serviceProduct = serviceProduct;
            _mapperProduct = mapperProduct;
        }
        public void Add(ProductRequestDTO productDto)
        {
            var product = _mapperProduct.MapperDtoToEntity(productDto);
            _serviceProduct.Add(product);
        }

        public void Delete(ProductRequestDTO productDto)
        {
            var product = _mapperProduct.MapperDtoToEntity(productDto);
            _serviceProduct.Delete(product);
        }

        public IEnumerable<ProductRequestDTO> GetAll()
        {
            var products = _serviceProduct.GetAll();
            return _mapperProduct.MapperListProductDto(products);
        }

        public ProductRequestDTO GetById(Guid id)
        {
            var product = _serviceProduct.GetById(id);
            return _mapperProduct.MapperEntityToDto(product);
        }

        public void Update(ProductRequestDTO productDto)
        {
            var product = _mapperProduct.MapperDtoToEntity(productDto);
            _serviceProduct.Update(product);
        }
    }
}