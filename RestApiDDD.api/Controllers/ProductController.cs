using Microsoft.AspNetCore.Mvc;
using RestApiDDD.Application.DTOs.Request;
using RestApiDDD.Application.DTOs.Request.Update;
using RestApiDDD.Application.DTOs.Response;
using RestApiDDD.Application.Interfaces;

namespace RestApiDDD.Api.Controllers
{
    [ApiController]
    [Route("/product/v1")]
    public class ProductController : Controller
    {
        private readonly IApplicationServiceProduct _applicationServiceProduct;

        public ProductController(IApplicationServiceProduct applicationServiceProduct)
        {
            _applicationServiceProduct = applicationServiceProduct;
        }

        [HttpPost]
        [Route("/register-product")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddProduct([FromBody] ProductRequestDTO productDto)
        {
            if (ModelState.IsValid)
            {
                var response = await _applicationServiceProduct.Add(productDto);

                if (response.Success)
                    return Created("Successfully registered.", response);

                return BadRequest(response);
            }

            return StatusCode(500);
        }
        
        [HttpGet]
        [Route("/show-all-products")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<IEnumerable<ProductResponseDTO>> GetAllProducts()
        {
            try
            {
                var response = _applicationServiceProduct.GetAll();

                return Ok(response);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("/show-one-product/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetOneProduct(Guid id)
        {
            if (ModelState.IsValid)
            {
                var response = await _applicationServiceProduct.GetById(id);

                if (response.Success)
                    return Ok(response);

                return BadRequest(response);   

            }

            return StatusCode(500);
        }

        [HttpDelete]
        [Route("/delete-product/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            if (ModelState.IsValid)
            {
                var response = await _applicationServiceProduct.Delete(id);

                if (response.Success)
                    return Ok(response);

                return BadRequest(response);

            }

            return StatusCode(500);
        }

        [HttpPut]
        [Route("/update-product/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateProdut(Guid id,
                                         [FromBody] ProductRequestUpdateDTO productDto)
        {
            if (ModelState.IsValid)
            {
                var response = await _applicationServiceProduct.Update(id, productDto);

                if (response.Success)
                    return Ok(response);

                return BadRequest(response);
            }

            return StatusCode(500);
        }
    }
}