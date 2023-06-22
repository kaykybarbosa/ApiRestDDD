using RestApiDDD.Application.DTOs.Request;

namespace RestApiDDD.Application.Interfaces
{
    public interface IApplicationServiceProduct
    {
        void Add(ProductRequestDTO productDto);
        void Delete(ProductRequestDTO productDto);
        void Update(ProductRequestDTO productDto);
        IEnumerable<ProductRequestDTO> GetAll();
        ProductRequestDTO GetById(Guid id);
    }
}