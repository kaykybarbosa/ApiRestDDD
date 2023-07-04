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
                Message = "Product added successfully.",
                Success = true,
            };

            A.CallTo(() => _applicationServiceProduct.Add(client)).Returns(Task.FromResult(response));

            //Act
            var result = await _productController.AddProduct(client);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<CreatedResult>().Which.Value.Should().BeEquivalentTo(response);
        }

        [Fact]
        public async Task ProductController_GetAllProduct_ReturnSuccess()
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
        public async Task ProductController_GetOneProduct_ReturnSuccess()
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
                Message = "Found successfully. ",
                Success = true
            };

            A.CallTo(() => _applicationServiceProduct.GetById(id)).Returns(Task.FromResult(response));

            //Act
            var result = await _productController.GetOneProduct(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>().Which.Value.Should().BeEquivalentTo(response);
        }

        [Fact]
        public async Task ProductController_DeleteProduct_ReturnSuccess()
        {
            //Arrange
            var id = Guid.NewGuid();

            BaseResponseDTO response = new()
            {
                Message = "Product deleted successfully.",
                Success = true
            };

            A.CallTo(() => _applicationServiceProduct.Delete(id)).Returns(Task.FromResult(response));

            //Act
            var result = await _productController.DeleteProduct(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>().Which.Value.Should().BeEquivalentTo(response);
        }

        [Fact]
        public async Task ProductController_UpdateProduc_ReturnSuccess()
        {
            //Arrange
            var id = Guid.NewGuid();

            var product = A.Fake<ProductRequestUpdateDTO>();

            BaseResponseDTO response = new()
            {
                Message = "Product updatind successfully.",
                Success = true
            };

            A.CallTo(() => _applicationServiceProduct.Update(id, product)).Returns(Task.FromResult(response));

            //Act
            var result = await _productController.UpdateProdut(id, product);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>().Which.Value.Should().BeEquivalentTo(response);
        }
    }
}
