using RestApiDDD.Application.DTOs;

namespace RestApiDDD.Application.Interfaces
{
    public interface ApplicationServiceClient
    {
        void Add(ClientDto clientDto);
        void Delete(ClientDto clientDto);
        void Update(ClientDto clientDto);
        IEnumerable<ClientDto> GetAll();
        ClientDto GetById(Guid id);
    }
}