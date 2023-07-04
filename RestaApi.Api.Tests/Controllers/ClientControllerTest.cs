using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using RestApiDDD.Api.Controllers;
using RestApiDDD.Application.DTOs.Request;
using RestApiDDD.Application.DTOs.Request.Update;
using RestApiDDD.Application.DTOs.Response;
using RestApiDDD.Application.Interfaces;
using RestApiDDD.Domain.Entities;

namespace RestaApi.Api.Tests.Controllers
{
    public class ClientControllerTest
    {
        private readonly IApplicationServiceClient _applicationServiceClient;
        private readonly ClientController _clientControler;

        public ClientControllerTest()
        {
            _applicationServiceClient = A.Fake<IApplicationServiceClient>();
            _clientControler = new ClientController(_applicationServiceClient);
        }

        [Fact]
        public async Task ClientController_AddClient_ReturnCreated()
        {
            //Arrange
            var clientResquest = A.Fake<ClientRequestDTO>();

            BaseResponseDTO response = new()
            {
                Message = "Client added successfully.",
                Success = true,
            };

            A.CallTo(() => _applicationServiceClient.Add(clientResquest))
                .Returns(Task.FromResult(response));

            //Act
            var result = await _clientControler.AddClient(clientResquest);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<CreatedResult>()
                .Which.Value.Should().BeEquivalentTo(response);
        }

        [Fact]
        public async Task ClientController_GetAllClient_ReturnOkResult()
        {
            //Arrange
            var listClient = A.Fake<IEnumerable<ClientResponseDTO>>();

            A.CallTo(() => _applicationServiceClient.GetAll())
                .Returns(Task.FromResult(listClient));

            //Act
            var result = await _clientControler.GetAllClient();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>()
                .Which.Value.Should().BeEquivalentTo(listClient);
        }

        [Fact]
        public async Task ClientController_GetOneClient_ReturnSuccess()
        {
            //Arrange
            var client = A.Fake<Client>();

            ClientResponseDTO response = new()
            {
                Id = client.Id,
                FirstName = client.FirstName,
                LastName = client.LastName,
                Email = client.Email,
                RegistrationDate = client.RegistrationDate,
                IsActive = client.IsActive,
            };

            A.CallTo(() => _applicationServiceClient.GetById(client.Id))
                .Returns(Task.FromResult(response));

            //Act
            var result = await _clientControler.GetOneClient(client.Id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>()
                           .Which.Value.Should().BeEquivalentTo(response);
        }

        [Fact]
        public async Task ClientController_DeleteClient_ReturnSuccess()
        {
            //Arrange
            var client = A.Fake<Client>();

            BaseResponseDTO response = new()
            {
                Message = "Client deleted successfully.",
                Success = true
            };

            A.CallTo(() => _applicationServiceClient.Delete(client.Id))
                .Returns(Task.FromResult(response));

            //Act
            var result = await _clientControler.DeleteClient(client.Id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>()
                .Which.Value.Should().BeEquivalentTo(response);
        }

        [Fact]
        public async Task ClientController_UpdateClient_ReturnSuccess()
        {
            //Arrange
            var id = Guid.NewGuid();

            var client = A.Fake<ClientRequestUpdateDTO>();

            BaseResponseDTO response = new()
            {
                Message = "Client updating successfully.",
                Success = true
            };

            A.CallTo(() => _applicationServiceClient.Update(id, client))
                .Returns(Task.FromResult(response));

            //Act
            var result = await _clientControler.UpdateClient(id, client);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>()
                .Which.Value.Should().BeEquivalentTo(response);
        }
    }
}
