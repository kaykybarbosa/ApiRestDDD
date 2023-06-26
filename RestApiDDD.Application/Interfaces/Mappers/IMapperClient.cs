using RestApiDDD.Application.DTOs.Request;
using RestApiDDD.Application.DTOs.Response;
using RestApiDDD.Domain.Entities;

namespace RestApiDDD.Application.Interfaces.Mappers
{
    public interface IMapperClient
    {
        Client MapperDtoToEntity(ClientRequestDTO clientDto);
        IEnumerable<ClientResponseDTO> MapperListClientDto(IEnumerable<Client> clients);
        ClientResponseDTO MapperEntityToDto(Client client);
    }
}