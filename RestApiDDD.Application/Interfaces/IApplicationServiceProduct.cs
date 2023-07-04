using RestApiDDD.Application.DTOs.Request;
using RestApiDDD.Application.DTOs.Request.Update;
using RestApiDDD.Application.DTOs.Response;

namespace RestApiDDD.Application.Interfaces
{
    public interface IApplicationServiceProduct
    {
        Task<BaseResponseDTO> Add(ProductRequestDTO productDto);
        Task<BaseResponseDTO> Delete(Guid id);
        Task<BaseResponseDTO> Update(Guid id, ProductRequestUpdateDTO productDto);
        Task<IEnumerable<ProductResponseDTO>> GetAll();
        Task<ProductResponseDTO> GetById(Guid id);
    }
}