using RestApiDDD.Application.DTOs;

namespace RestApiDDD.Application.Interfaces
{
    public interface IApplicationServiceProduct
    {
        void Add(ProductDto productDto);
        void Delete(ProductDto productDto);
        void Update(ProductDto productDto);
        IEnumerable<ProductDto> GetAll();
        ProductDto GetById(Guid id);
    }
}