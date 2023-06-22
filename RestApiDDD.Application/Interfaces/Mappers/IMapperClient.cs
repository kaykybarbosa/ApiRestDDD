using RestApiDDD.Application.DTOs.Request;
using RestApiDDD.Application.DTOs.Response;
using RestApiDDD.Domain.Entities;

namespace RestApiDDD.Application.Interfaces.Mappers
{
    public interface IMapperClient
    {
        Client MapperDtoToEntity(ClientRequestDTO clientDto);
        IEnumerable<ClientReponseDTO> MapperListClientDto(IEnumerable<Client> clients);
        ClientReponseDTO MapperEntityToDto(Client client);
        ClientRequestDTO MapperReponseToRequest(ClientReponseDTO clientResponse);
    }
}