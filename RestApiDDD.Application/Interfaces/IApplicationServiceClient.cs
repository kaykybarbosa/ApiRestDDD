using RestApiDDD.Application.DTOs.Request;
using RestApiDDD.Application.DTOs.Request.Update;
using RestApiDDD.Application.DTOs.Response;

namespace RestApiDDD.Application.Interfaces
{
    public interface IApplicationServiceClient
    {
        Task<BaseResponseDTO> Add(ClientRequestDTO clientDto);
        Task<BaseResponseDTO> Delete(Guid id);
        Task<BaseResponseDTO> Update(Guid id, ClientRequestUpdateDTO clientDto);
        IEnumerable<ClientResponseDTO> GetAll();
        Task<ClientResponseDTO> GetById(Guid id);
        BaseResponseDTO GetByEmail(string email);
    }
}