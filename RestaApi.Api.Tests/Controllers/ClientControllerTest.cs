using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
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
        private readonly ClientController _clientController;

        public ClientControllerTest()
        {
            _applicationServiceClient = A.Fake<IApplicationServiceClient>();
            _clientController = new ClientController(_applicationServiceClient);
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
            var result = await _clientController.AddClient(clientResquest);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<CreatedResult>()
                .Which.Value.Should().BeEquivalentTo(response);
        }

        [Fact]
        public async Task ClientController_AddClient_ReturnBadRequest()
        {
            //Arrange
            var clientInexistent = A.Fake<ClientRequestDTO>();

            var error = A.Fake<Exception>();

            BaseResponseDTO response = new()
            {
                Message = "Error while saving client.",
                Success = false,
                Error = error.Message
            };

            A.CallTo(() => _applicationServiceClient.Add(clientInexistent)).Returns(Task.FromResult(response));

            //Act
            var result = await _clientController.AddClient(clientInexistent);

            //Assert
            result.Should().BeOfType<BadRequestObjectResult>().Which.StatusCode.Should().Be(400);
        }

        [Fact]
        public async Task ClientController_GetAllClient_ReturnSuccess()
        {
            //Arrange
            var listClient = A.Fake<IEnumerable<ClientResponseDTO>>();

            A.CallTo(() => _applicationServiceClient.GetAll())
                .Returns(Task.FromResult(listClient));

            //Act
            var result = await _clientController.GetAllClient();

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
            var result = await _clientController.GetOneClient(client.Id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>()
                           .Which.Value.Should().BeEquivalentTo(response);
        }

        [Fact]
        public async Task ClientController_GetOneClient_ReturnNotFound()
        {
            //Arrange 
            var idInexistent = Guid.NewGuid();

            ClientResponseDTO response = new()
            {
                Message = "Client by Id not found.",
                Success = false,
            };

            A.CallTo(() => _applicationServiceClient.GetById(idInexistent)).Returns(Task.FromResult(response));

            //Act
            var result = await _clientController.GetOneClient(idInexistent);

            //Assert
            result.Should().BeOfType<NotFoundObjectResult>().Which.StatusCode.Should().Be(404);
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
            var result = await _clientController.DeleteClient(client.Id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>()
                .Which.Value.Should().BeEquivalentTo(response);
        }

        [Fact]
        public async Task ClientController_DeleteClient_ReturnBadRequest()
        {
            //Arrange
            var idInvalid = Guid.NewGuid();

            var error = A.Fake<Exception>();

            BaseResponseDTO response = new()
            {
                Message = "Error while deleting client",
                Success = false,
                Error = error.Message
            };

            A.CallTo(() => _applicationServiceClient.Delete(idInvalid)).Returns(Task.FromResult(response));

            //Act
            var result = await _clientController.DeleteClient(idInvalid);

            //Assert
            result.Should().BeOfType<BadRequestObjectResult>().Which.StatusCode.Should().Be(400);
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
            var result = await _clientController.UpdateClient(id, client);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>()
                .Which.Value.Should().BeEquivalentTo(response);
        }

        [Fact]
        public async Task ClientController_UpdateClient_ReturnBadRequest()
        {
            //Arrange
            var idInvalid = Guid.NewGuid();

            var clientInvalid = A.Fake<ClientRequestUpdateDTO>();

            var error = A.Fake<Exception>();

            BaseResponseDTO response = new()
            {
                Message = "Error while updating client.",
                Success = false,
                Error = error.Message
            };

            A.CallTo(() => _applicationServiceClient.Update(idInvalid, clientInvalid)).Returns(Task.FromResult(response));

            //Act
            var result = await _clientController.UpdateClient(idInvalid, clientInvalid);

            //Assert
            result.Should().BeOfType<BadRequestObjectResult>().Which.StatusCode.Should().Be(400);
        }
    }
}
