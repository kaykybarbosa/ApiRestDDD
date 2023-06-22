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

        public ClientReponseDTO MapperEntityToDto(Client client)
        {
            var clientDto = new ClientReponseDTO()
            {
                Id = client.Id,
                FirstName = client.FirstName,
                LastName = client.LastName,
                Email = client.Email,
                RegistrationDate = client.RegistrationDate,
                IsActive = client.IsActive
            };

            return clientDto;
        }

        public IEnumerable<ClientReponseDTO> MapperListClientDto(IEnumerable<Client> clients)
        {
            var clientsDto = clients.Select(c => new ClientReponseDTO
            {
                Id = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Email = c.Email,
                RegistrationDate = c.RegistrationDate,
                IsActive = c.IsActive
            });

            return clientsDto;
        }

        //public ClientRequestDTO MapperReponseToRequest(ClientReponseDTO clientResponse)
        //{
        //    var clientRequest = new ClientRequestDTO()
        //    {

        //    }
        //}
    }
}