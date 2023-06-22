using RestApiDDD.Application.DTOs.Request;
using RestApiDDD.Application.DTOs.Response;
using RestApiDDD.Application.Interfaces;
using RestApiDDD.Application.Interfaces.Mappers;
using RestApiDDD.Domain.Core.Interfaces.Services;

namespace RestApiDDD.Application
{
    public class ApplicationServiceClient : IApplicationServiceClient
    {
        private IServiceClient _serviceClient;
        private IMapperClient _mapperClient;

        public ApplicationServiceClient(IServiceClient serviceClient,
                                        IMapperClient mapperClient)
        {
            _serviceClient = serviceClient;
            _mapperClient = mapperClient;
        }
        public void Add(ClientRequestDTO clientDto)
        {
            var client = _mapperClient.MapperDtoToEntity(clientDto);
            _serviceClient.Add(client);
        }

        public void Delete(ClientRequestDTO clientDto)
        {
            var client = _mapperClient.MapperDtoToEntity(clientDto);
            _serviceClient.Delete(client);
        }

        public IEnumerable<ClientReponseDTO> GetAll()
        {
            var clients = _serviceClient.GetAll();
            return _mapperClient.MapperListClientDto(clients);
        }

        public ClientReponseDTO GetById(Guid id)
        {
            var client = _serviceClient.GetById(id);
            return _mapperClient.MapperEntityToDto(client);
        }

        public void Update(ClientRequestDTO clientDto)
        {
            var client = _mapperClient.MapperDtoToEntity(clientDto);
            _serviceClient.Update(client);

        }
    }
}