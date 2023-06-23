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
        public async Task<BaseResponseDTO> Add(ClientRequestDTO clientDto)
        {
            try
            {
                var client = _mapperClient.MapperDtoToEntity(clientDto);
                
                await _serviceClient.Add(client!);

                return new BaseResponseDTO()
                {
                    Message = "Client added successfully.",
                    Success = true
                };
            }
            catch (Exception e)
            {
                return new BaseResponseDTO()
                {
                    Message = "Error while saving client.",
                    Success = false,
                    Error = e.Message
                };
            }
        }

        public async Task<BaseResponseDTO> Delete(Guid id)
        {
            try
            {
                var client = _serviceClient.GetById(id);
                BaseResponseDTO response = new();

                if(client == null) 
                {
                    response.Message = "Client by Id not found.";
                    response.Success = false;
                }
                else
                {
                    await _serviceClient.Delete(client.Result);

                    response.Message = "Client deleted successfully.";
                    response.Success = true;
                }

                return response;
            }
            catch (Exception e)
            {
                return new BaseResponseDTO()
                {
                    Message = "Error while deleting client.",
                    Success = false,
                    Error = e.Message
                };
            }
        }

        public IEnumerable<ClientResponseDTO> GetAll()
        {
            var clients = _serviceClient.GetAll();

            return _mapperClient.MapperListClientDto(clients);
        }

        public async Task<ClientResponseDTO> GetById(Guid id)
        {
            try
            {
                var clientFound = await _serviceClient.GetById(id);

                if (clientFound == null)
                    return new ClientResponseDTO()
                    {
                        Message = "Cliend by Id not found",
                        Success = false
                    };

                return new ClientResponseDTO()
                {
                    Id = clientFound.Id,
                    FirstName = clientFound.FirstName,
                    LastName = clientFound.LastName,
                    Email = clientFound.Email,
                    RegistrationDate = clientFound.RegistrationDate,
                    IsActive = clientFound.IsActive,
                    Message = "Found successfully.",
                    Success = true
                };


            }
            catch (Exception e)
            {

                return new ClientResponseDTO()
                {
                    Message = "Error occurred while fetching the client.",
                    Success = false,
                    Error = e.Message
                };
            }
        }

        public async Task<ClientResponseDTO> Update(Guid id, ClientRequestDTO clientDto)
        {
            try
            {
                var clientFound = await _serviceClient.GetById(id);
                
                if(clientFound == null)
                {
                    return new ClientResponseDTO()
                    {
                        Message = "Client by Id not found.",
                        Success = false
                    };
                }

                //var client = _mapperClient.MapperDtoToNewEntity(clientDto);

                clientFound.FirstName = clientDto.FirstName;
                clientFound.LastName = clientDto.LastName;
                clientFound.Email = clientDto.Email;

                await _serviceClient.Update(clientFound!);

                return new ClientResponseDTO()
                {
                    Id = clientFound.Id,
                    FirstName = clientFound.FirstName,
                    LastName = clientFound.LastName,
                    Email = clientFound.Email,
                    RegistrationDate =  clientFound.RegistrationDate,
                    IsActive = clientFound.IsActive,
                    Message = "Client updating successfully.",
                    Success = true
                };
            }
            catch (Exception e)
            {
                return new ClientResponseDTO()
                {
                    Message = "Error occurred while updating client.",
                    Success = false,
                    Error = e.Message
                };
            }
        }
    }
}