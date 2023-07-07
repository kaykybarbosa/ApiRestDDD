using Azure.Core;
using FakeItEasy;
using FluentAssertions;
using Microsoft.Identity.Client;
using RestApiDDD.Application;
using RestApiDDD.Application.DTOs.Request;
using RestApiDDD.Application.DTOs.Request.Update;
using RestApiDDD.Application.DTOs.Response;
using RestApiDDD.Application.Interfaces.Mappers;
using RestApiDDD.Domain.Core.Interfaces.Services;
using RestApiDDD.Domain.Entities;

namespace RestaApi.Api.Tests.Services
{
    public class ApplicationServiceClientTest
    {
        private readonly IServiceClient _serviceClient;
        private readonly IMapperClient _mapperClient;
        private readonly ApplicationServiceClient _applicationClient;

        public ApplicationServiceClientTest()
        {
            _serviceClient = A.Fake<IServiceClient>();
            _mapperClient = A.Fake<IMapperClient>();
            _applicationClient = new ApplicationServiceClient(_serviceClient, _mapperClient);
        }

        [Fact]
        public async Task ApplicationServiceClient_Add_ReturnSuccess()
        {
            //Arrange
            var client = A.Fake<Client>();
            var value = A.Fake<Task<Client>>();
            value = null;

            ClientRequestDTO request = new() 
            {
                FirstName = "Test",
                LastName = "Test",
                Email = "Test"
            };

            BaseResponseDTO response = new()
            {
                Message = "Client added successfully.",
                Success = true,
            };

            A.CallTo(() => _mapperClient.MapperDtoToEntity(request)).Returns(new Client());
            A.CallTo(() => _serviceClient.GetByEmail(A<String>.Ignored)).Returns(value);
            A.CallTo(() => _serviceClient.Add(client)).Returns(client);

            //Act
            var result = await _applicationClient.Add(request);

            //Assert
            result.Should().BeEquivalentTo(response);
            result.Success.Should().BeTrue();
        }

        [Fact]
        public async Task ApplicationServiceClient_GetById_ReturnSuccess()
        {
            //Arrange
            var id = Guid.NewGuid();
            var client = A.Fake<Client>();

            ClientResponseDTO response = new()
            {
                Id = client.Id,
                FirstName = client.FirstName,
                LastName = client.LastName,
                Email = client.Email,
                IsActive = client.IsActive,
                Message = "Found successfully.",
                Success = true
            };

            A.CallTo(() => _mapperClient.MapperEntityToDto(client)).Returns(response);
            A.CallTo(() => _serviceClient.GetById(id)).Returns(Task.FromResult(client));

            //Act
            var result = await _applicationClient.GetById(id);

            //Assert
            result.Should().BeEquivalentTo(response);
            result.Success.Should().BeTrue();
        }

        [Fact]
        public async Task ApplicationServiceClient_GetAll_ReturnSuccess()
        {
            //Arrange
            var clientsResponse = A.Fake<IEnumerable<ClientResponseDTO>>();
            var clients = A.Fake<IEnumerable<Client>>();

            A.CallTo(() => _serviceClient.GetAll()).Returns(Task.FromResult(clients));
            A.CallTo(() => _mapperClient.MapperListClientDto(clients)).Returns(clientsResponse);

            //Act
            var result = await _applicationClient.GetAll();

            //Assert
            result.Should().BeEquivalentTo(clientsResponse);
        }

        [Fact]
        public async Task ApplicationServiceClient_Delete_ReturnSuccess()
        {
            //Arrange
            var id = Guid.NewGuid();
            var client = A.Fake<Client>();

            BaseResponseDTO response = new() 
            {
                Message = "Client deleted successfully.",
                Success = true
            };

            A.CallTo(() => _serviceClient.GetById(id)).Returns(Task.FromResult(client));
            A.CallTo(() => _serviceClient.Delete(client)).Returns(Task.FromResult(client));

            //Act
            var result = await _applicationClient.Delete(id);

            //Assert
            result.Should().BeEquivalentTo(response);
            result.Success.Should().BeTrue();
        }

        [Fact]
        public async Task ApplicationServiceClient_Update_ReturnSuccess()
        {
            //Arrange
            var id = Guid.NewGuid();
            var client = A.Fake<Client>();

            ClientRequestUpdateDTO request = new()
            {
                FirstName = client.FirstName,
                LastName = client.LastName,
                Email = client.Email,
                IsActive = client.IsActive,
            };

            BaseResponseDTO response = new()
            {
                Message = "Client updating successfully.",
                Success = true
            };

            A.CallTo(() => _serviceClient.Update(client)).Returns(Task.FromResult(client));

            //Act
            var result = await _applicationClient.Update(id, request);

            //Assert
            result.Should().BeEquivalentTo(response);
            result.Success.Should().BeTrue();
        }
    }
}
