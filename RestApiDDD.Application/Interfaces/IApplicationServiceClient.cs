using RestApiDDD.Application.DTOs.Request;
using RestApiDDD.Application.DTOs.Response;
using RestApiDDD.Domain.Entities;

namespace RestApiDDD.Application.Interfaces
{
    public interface IApplicationServiceClient
    {
        void Add(ClientRequestDTO clientDto);
        void Delete(ClientRequestDTO client);
        void Update(ClientRequestDTO clientDto);
        IEnumerable<ClientReponseDTO> GetAll();
        ClientReponseDTO GetById(Guid id);
    }
}