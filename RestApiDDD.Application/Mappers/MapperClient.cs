using RestApiDDD.Application.DTOs.Request;
using RestApiDDD.Application.DTOs.Response;
using RestApiDDD.Application.Interfaces.Mappers;
using RestApiDDD.Domain.Entities;

namespace RestApiDDD.Application.Mappers
{
    public class MapperClient : IMapperClient
    {
        public Client MapperDtoToEntity(ClientRequestDTO clientDto)
        {
            var client = new Client()
            {
                FirstName = clientDto.FirstName,
                LastName = clientDto.LastName,
                Email = clientDto.Email
            };

            return client;
        }

        public ClientResponseDTO MapperEntityToDto(Client client)
        {
            var clientDto = new ClientResponseDTO()
            {
                Id = client.Id,
                FirstName = client.FirstName,
                LastName = client.LastName,
                Email = client.Email,
                IsActive = client.IsActive
            };

            return clientDto;
        }

        public IEnumerable<ClientResponseDTO> MapperListClientDto(IEnumerable<Client> clients)
        {
            var clientsDto = clients.Select(c => new ClientResponseDTO
            {
                Id = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Email = c.Email,
                IsActive = c.IsActive
            });

            return clientsDto;
        }
    }
}