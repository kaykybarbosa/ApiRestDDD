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
            var client = A.Fake<ClientRequestDTO>();

            BaseResponseDTO response = new()
            {
                Message = "Successfully registered.",
                Success = true,
            };

            A.CallTo(() => _applicationServiceClient.Add(A<ClientRequestDTO>.Ignored))
                .Returns(Task.FromResult(response));

            //Act
            var result = await _clientController.AddClient(client);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<CreatedResult>()
                .Which.Value.Should().BeEquivalentTo(response);
        }

        [Fact]
        public async Task ClientController_AddClient_ReturnBadRequest()
        {
            //Arrange
            var clientInvalid = A.Fake<ClientRequestDTO>();

            var error = A.Fake<Exception>();

            BaseResponseDTO response = new()
            {
                Message = "Message.",
                Success = false,
                Error = error.Message
            };

            A.CallTo(() => _applicationServiceClient.Add(A<ClientRequestDTO>.Ignored)).Returns(Task.FromResult(response));

            //Act
            var result = await _clientController.AddClient(clientInvalid);

            //Assert
            result.Should().BeOfType<BadRequestObjectResult>().Which.StatusCode.Should().Be(400);
        }

        [Fact]
        public async Task ClientController_GetAllClient_ReturnOKResult()
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
        public async Task ClientController_GetOneClient_ReturnOkResult()
        {
            //Arrange
            var client = A.Fake<Client>();

            ClientResponseDTO response = new()
            {
                Id = client.Id,
                FirstName = client.FirstName,
                LastName = client.LastName,
                Email = client.Email,
                IsActive = client.IsActive,
                Message = "Message.",
                Success = true
            };

            A.CallTo(() => _applicationServiceClient.GetById(A<Guid>.Ignored))
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

            var error = A.Fake<Exception>();

            ClientResponseDTO response = new()
            {
                Message = "Message.",
                Success = false,
                Error = error.Message
            };

            A.CallTo(() => _applicationServiceClient.GetById(A<Guid>.Ignored)).Returns(Task.FromResult(response));

            //Act
            var result = await _clientController.GetOneClient(idInexistent);

            //Assert
            result.Should().BeOfType<NotFoundObjectResult>().Which.StatusCode.Should().Be(404);
        }

        [Fact]
        public async Task ClientController_DeleteClient_ReturnOkResult()
        {
            //Arrange
            var id = Guid.NewGuid();

            BaseResponseDTO response = new()
            {
                Message = "Message.",
                Success = true
            };

            A.CallTo(() => _applicationServiceClient.Delete(A<Guid>.Ignored))
                .Returns(Task.FromResult(response));

            //Act
            var result = await _clientController.DeleteClient(id);

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
                Message = "Message.",
                Success = false,
                Error = error.Message
            };

            A.CallTo(() => _applicationServiceClient.Delete(A<Guid>.Ignored)).Returns(Task.FromResult(response));

            //Act
            var result = await _clientController.DeleteClient(idInvalid);

            //Assert
            result.Should().BeOfType<BadRequestObjectResult>().Which.StatusCode.Should().Be(400);
        }

        [Fact]
        public async Task ClientController_UpdateClient_ReturnOkResult()
        {
            //Arrange
            var id = Guid.NewGuid();

            var client = A.Fake<ClientRequestUpdateDTO>();

            BaseResponseDTO response = new()
            {
                Message = "Message.",
                Success = true
            };

            A.CallTo(() => _applicationServiceClient.Update(A<Guid>.Ignored, A<ClientRequestUpdateDTO>.Ignored))
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
                Message = "Message.",
                Success = false,
                Error = error.Message
            };

            A.CallTo(() => _applicationServiceClient.Update(A<Guid>.Ignored, A<ClientRequestUpdateDTO>.Ignored)).Returns(Task.FromResult(response));

            //Act
            var result = await _clientController.UpdateClient(idInvalid, clientInvalid);

            //Assert
            result.Should().BeOfType<BadRequestObjectResult>().Which.StatusCode.Should().Be(400);
        }
    }
}