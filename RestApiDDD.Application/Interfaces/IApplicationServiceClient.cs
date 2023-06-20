using RestApiDDD.Application.DTOs;

namespace RestApiDDD.Application.Interfaces
{
    public interface IApplicationServiceClient
    {
        void Add(ClientDto clientDto);
        void Delete(ClientDto clientDto);
        void Update(ClientDto clientDto);
        IEnumerable<ClientDto> GetAll();
        ClientDto GetById(Guid id);
    }
}