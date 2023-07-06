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
    public class ProductControllerTest
    {
        private readonly IApplicationServiceProduct _applicationServiceProduct;
        private readonly ProductController _productController;
        public ProductControllerTest()
        {
            _applicationServiceProduct = A.Fake<IApplicationServiceProduct>();
            _productController = new ProductController(_applicationServiceProduct);
        }

        [Fact]
        public async Task ProductController_AddProduct_ReturnCreated()
        {
            //Arrange
            var client = A.Fake<ProductRequestDTO>();

            BaseResponseDTO response = new()
            {
                Message = "Successfully registered.",
                Success = true,
            };

            A.CallTo(() => _applicationServiceProduct.Add(A<ProductRequestDTO>.Ignored)).Returns(Task.FromResult(response));

            //Act
            var result = await _productController.AddProduct(client);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<CreatedResult>().Which.Value.Should().BeEquivalentTo(response);
        }

        [Fact]
        public async Task ProductController_AddProduct_ReturnBadRequest()
        {
            //Arrange
            var productInvalid = A.Fake<ProductRequestDTO>();

            var error = A.Fake<Exception>();

            BaseResponseDTO response = new() 
            {
                Message = "Message.",
                Success = false,
                Error = error.Message
            };

            A.CallTo(() => _applicationServiceProduct.Add(A<ProductRequestDTO>.Ignored)).Returns(Task.FromResult(response));

            //Act
            var result = await _productController.AddProduct(productInvalid);

            //Assert
            result.Should().BeOfType<BadRequestObjectResult>().Which.StatusCode.Should().Be(400);
        }

        [Fact]
        public async Task ProductController_GetAllProduct_ReturnOkResult()
        {
            //Arrange
            var productList = A.Fake<IEnumerable<ProductResponseDTO>>();

            A.CallTo(() => _applicationServiceProduct.GetAll()).Returns(Task.FromResult(productList));

            //Act
            var result = await _productController.GetAllProducts();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>().Which.Value.Should().BeEquivalentTo(productList);
        }

        [Fact]
        public async Task ProductController_GetOneProduct_ReturnOkResult()
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
                Message = "Message.",
                Success = true
            };

            A.CallTo(() => _applicationServiceProduct.GetById(A<Guid>.Ignored)).Returns(Task.FromResult(response));

            //Act
            var result = await _productController.GetOneProduct(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>().Which.Value.Should().BeEquivalentTo(response);
        }

        [Fact]
        public async Task ProductController_GetOneProduct_ReturnNotFound()
        {
            //Arrange
            var idInexistent = Guid.NewGuid();

            ProductResponseDTO response = new()
            {
                Message = "Message.",
                Success = false
            };

            A.CallTo(() => _applicationServiceProduct.GetById(A<Guid>.Ignored)).Returns(Task.FromResult(response));

            //Act
            var result = await _productController.GetOneProduct(idInexistent);

            //Assert
            result.Should().BeOfType<NotFoundObjectResult>().Which.StatusCode.Should().Be(404);
        }

        [Fact]
        public async Task ProductController_DeleteProduct_ReturnOkResult()
        {
            //Arrange
            var id = Guid.NewGuid();

            BaseResponseDTO response = new()
            {
                Message = "Message.",
                Success = true
            };

            A.CallTo(() => _applicationServiceProduct.Delete(A<Guid>.Ignored)).Returns(Task.FromResult(response));

            //Act
            var result = await _productController.DeleteProduct(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>().Which.Value.Should().BeEquivalentTo(response);
        }

        [Fact]
        public async Task ProductController_DeleteProduct_ReturnNotFound()
        {
            //Arrange
            var idInexistent = Guid.NewGuid();

            BaseResponseDTO response = new() 
            {
                Message = "Message.",
                Success = false
            };

            A.CallTo(() => _applicationServiceProduct.Delete(A<Guid>.Ignored)).Returns(Task.FromResult(response));

            //Act
            var result = await _productController.DeleteProduct(idInexistent);

            //Assert
            result.Should().BeOfType<NotFoundObjectResult>().Which.StatusCode.Should().Be(404);
        }

        [Fact]
        public async Task ProductController_UpdateProduc_ReturnOkResult()
        {
            //Arrange
            var id = Guid.NewGuid();

            var product = A.Fake<ProductRequestUpdateDTO>();

            BaseResponseDTO response = new()
            {
                Message = "Message.",
                Success = true
            };

            A.CallTo(() => _applicationServiceProduct.Update(A<Guid>.Ignored, A<ProductRequestUpdateDTO>.Ignored))
                .Returns(Task.FromResult(response));

            //Act
            var result = await _productController.UpdateProdut(id, product);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>().Which.Value.Should().BeEquivalentTo(response);
        }

        [Fact]
        public async Task ProductController_UpdateProduct_ReturnBadRequest()
        {
            //Arrange
            var id = Guid.NewGuid();

            var productInvalid = A.Fake<ProductRequestUpdateDTO>();

            var error = A.Fake<Exception>();

            BaseResponseDTO response = new()
            {
                Message = "Message.",
                Success = false,
                Error = error.Message
            };

            A.CallTo(() => _applicationServiceProduct.Update(A<Guid>.Ignored, A<ProductRequestUpdateDTO>.Ignored))
                .Returns(Task.FromResult(response));

            //Act
            var result = await _productController.UpdateProdut(id, productInvalid);

            //Assert
            result.Should().BeOfType<BadRequestObjectResult>().Which.StatusCode.Should().Be(400);
        }
    }
}