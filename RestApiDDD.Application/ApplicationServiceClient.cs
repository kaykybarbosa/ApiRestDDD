using RestApiDDD.Application.DTOs.Request;
using RestApiDDD.Application.DTOs.Request.Update;
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
                var emailNoExist = GetByEmail(clientDto.Email);

                if(emailNoExist.Success)
                {
                    var client = _mapperClient.MapperDtoToEntity(clientDto);
                    await _serviceClient.Add(client!);

                    return new BaseResponseDTO(message: "Client added successfully.", success: true);
                }
                else
                {
                    return emailNoExist;
                }
            }
            catch (Exception e)
            {
                return new BaseResponseDTO(message: "Error while saving client.", success: false, error: e.Message);
            }
        }
        
        public async Task<BaseResponseDTO> Delete(Guid id)
        {
            try
            {
                BaseResponseDTO response = new();
                var client = _serviceClient.GetById(id);

                if(client == null) 
                {
                    response.Message = "Client with this Id not found.";
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
                return new BaseResponseDTO(message: "Error while deleting client.", success: false, error: e.Message);
            }
        }

        public async Task<IEnumerable<ClientResponseDTO>> GetAll()
        {
            var clients = await _serviceClient.GetAll();

            return _mapperClient.MapperListClientDto(clients);
        }

        public BaseResponseDTO GetByEmail(string email)
        {
            BaseResponseDTO response = new();
            var clientExist = _serviceClient.GetAll().Result.Where(e => e.Email == email.Trim());


            if (clientExist == null)
            {
                response.Success = true;
            }
            else
            {
                response.Message = "Client with this Email already exists.";
                response.Success = false;
            }

            return response;
        }

        public async Task<ClientResponseDTO> GetById(Guid id)
        {
            try
            {
                ClientResponseDTO response = new();
                var clientFound = await _serviceClient.GetById(id);

                if (clientFound == null) { 
                    response.Message = "Cliend with this Id not found";
                    response.Success = false;
                }
                else 
                {
                    response.Id = clientFound.Id;
                    response.FirstName = clientFound.FirstName;
                    response.LastName = clientFound.LastName;
                    response.Email = clientFound.Email;
                    response.RegistrationDate = clientFound.RegistrationDate;
                    response.IsActive = clientFound.IsActive;
                    response.Message = "Found successfully.";
                    response.Success = true;
                }

                return response;
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

        public async Task<BaseResponseDTO> Update(Guid id, ClientRequestUpdateDTO clientDto)
        {
            try
            {
                BaseResponseDTO response = new();
                var clientFound = await _serviceClient.GetById(id);

                if (clientFound == null)
                {
                    response.Message = "Client bywith this Id not found.";
                    response.Success = false;
                }
                else
                {
                    clientFound.FirstName = clientDto.FirstName;
                    clientFound.LastName = clientDto.LastName;
                    clientFound.Email = clientDto.Email;
                    clientFound.IsActive = clientDto.IsActive;

                    await _serviceClient.Update(clientFound!);

                    response.Message = "Client updating successfully.";
                    response.Success = true;
                }

                return response;

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