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
    public class ApplicationServiceProductTest
    {
        private readonly IServiceProduct _serviceProduct;
        private readonly IMapperProduct _mapperProduct;
        private readonly ApplicationServiceProduct _applicationProduct;

        public ApplicationServiceProductTest()
        {
            _serviceProduct = A.Fake<IServiceProduct>();
            _mapperProduct = A.Fake<IMapperProduct>();
            _applicationProduct = new ApplicationServiceProduct(_serviceProduct, _mapperProduct);
        }

        [Fact]
        public async Task ApplicationServiceProduct_Add_ReturnSuccess()
        {
            //Arrange 
            var product = A.Fake<Product>();

            ProductRequestDTO request = new() 
            {
                Name = product.Name,
                Price = product.Price
            };

            BaseResponseDTO response = new()
            {
                Message = "Product added successfully.",
                Success = true
            };

            A.CallTo(() => _serviceProduct.Add(product)).Returns(Task.FromResult(product));
            A.CallTo(() => _mapperProduct.MapperDtoToEntity(request)).Returns(new Product());

            //Act
            var result = await _applicationProduct.Add(request);

            //Assert
            result.Should().BeEquivalentTo(response);
            result.Success.Should().BeTrue();
        }

        [Fact]
        public async Task ApplicationServiceProduct_GetAll_ReturnSuccess()
        {
            //Arrange
            var products = A.Fake<IEnumerable<Product>>();
            var productsDTO = A.Fake<IEnumerable<ProductResponseDTO>>();

            A.CallTo(() => _serviceProduct.GetAll()).Returns(Task.FromResult(products));
            A.CallTo(() => _mapperProduct.MapperListProductDto(products)).Returns(productsDTO);

            //Act
            var result = await _applicationProduct.GetAll();

            //Assert
            result.Should().BeEquivalentTo(productsDTO);
        }

        [Fact]
        public async Task ApplicationServiceProduct_GetById_ReturnSuccess()
        {
            //Arrange
            var id = Guid.NewGuid();
            var product = A.Fake<Product>();

            ProductResponseDTO response = new() 
            { 
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                IsAvaiable = product.IsAvaiable,
                Message = "Found successfully.",
                Success = true
            };

            A.CallTo(() => _serviceProduct.GetById(id)).Returns(product);
            A.CallTo(() => _mapperProduct.MapperEntityToDto(product)).Returns(new ProductResponseDTO());

            //Act
            var result = await _applicationProduct.GetById(id);

            //Assert
            result.Should().BeEquivalentTo(response);
            result.Success.Should().BeTrue();
        }

        [Fact]
        public async Task ApplicationServiceProduct_GetById_ReturnError()
        {
            //Arrange
            var idInexistent = Guid.NewGuid();
            Product productNull = null;

            ProductResponseDTO response = new()
            {
                Message = "Product by Id not found.",
                Success = false
            };

            A.CallTo(() => _serviceProduct.GetById(idInexistent)).Returns(Task.FromResult(productNull));

            //Act
            var result = await _applicationProduct.GetById(idInexistent);

            //Assert
            result.Should().BeEquivalentTo(response);
            result.Success.Should().BeFalse();
        }

        [Fact]
        public async Task ApplicationServiceProduct_Delete_ReturnSuccess()
        {
            //Arrange
            var id = Guid.NewGuid();
            var product = A.Fake<Product>();

            BaseResponseDTO response = new()
            {
                Message = "Product deleted successfully.",
                Success = true
            };

            A.CallTo(() => _serviceProduct.Delete(product)).Returns(Task.FromResult(product));
            A.CallTo(() => _serviceProduct.GetById(id)).Returns(Task.FromResult(product));

            //Act
            var result = await _applicationProduct.Delete(id);

            //Assert
            result.Should().BeEquivalentTo(response);
            result.Success.Should().BeTrue();
        }

        [Fact]
        public async Task ApplicationServiceProduct_Delete_ReturnError()
        {
            //Arrange
            var idInexistent = Guid.NewGuid();
            Product? nullProduct = null;

            BaseResponseDTO response = new()
            {
                Message = "Product by Id not found.",
                Success = false
            };

            A.CallTo(() => _serviceProduct.GetById(idInexistent)).Returns(nullProduct);

            //Act
            var result = await _applicationProduct.Delete(idInexistent);

            //Asseret
            result.Should().BeEquivalentTo(response);
            result.Success.Should().BeFalse();
        }

        [Fact]
        public async Task ApplicationServiceProduct_Update_ReturnSuccess()
        {
            //Arrange
            var id = Guid.NewGuid();
            var product = A.Fake<Product>();

            ProductRequestUpdateDTO request = new()
            {
                Name = product.Name,
                Price = product.Price,
                IsAvaiable = product.IsAvaiable
            };

            BaseResponseDTO response = new()
            {
                Message = "Product updating successfully.",
                Success = true
            };

            A.CallTo(() => _serviceProduct.Update(product)).Returns(Task.FromResult(product));
            A.CallTo(() => _serviceProduct.GetById(id)).Returns(Task.FromResult(product));

            //Act 
            var result = await _applicationProduct.Update(id, request);

            //Assert
            result.Should().BeEquivalentTo(response);
            result.Success.Should().BeTrue();
        }

        [Fact]
        public async Task ApplicationServiceProduct_Update_ReturnError()
        {
            //Arrange
            var idInexistent = Guid.NewGuid();
            Product? nullProduct = null;

            ProductRequestUpdateDTO request = new()
            {
                Name = "Test",
                Price = 0,
                IsAvaiable = false
            };

            BaseResponseDTO response = new()
            {
                Message = "Product by Id not found.",
                Success = false
            };

            A.CallTo(() => _serviceProduct.GetById(idInexistent)).Returns(nullProduct);

            //Act
            var result = await _applicationProduct.Update(idInexistent,request);

            //Assert
            result.Should().BeEquivalentTo(response);
            result.Success.Should().BeFalse();
        }
    }
}
