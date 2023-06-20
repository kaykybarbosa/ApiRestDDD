using RestApiDDD.Application.DTOs;
using RestApiDDD.Domain.Entities;

namespace RestApiDDD.Application.Interfaces.Mappers
{
    public interface IMapperClient
    {
        Client MapperDtoToEntity(ClientDto clientDto);
        IEnumerable<ClientDto> MapperListClientDto(IEnumerable<Client> clients);
        ClientDto MapperEntityToDto(Client client);
    }
}