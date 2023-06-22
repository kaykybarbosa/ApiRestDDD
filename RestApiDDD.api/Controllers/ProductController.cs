using Microsoft.AspNetCore.Mvc;
using RestApiDDD.Application.DTOs;
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
        public ActionResult AddProduct([FromBody] ProductDto productDto)
        {
            _applicationServiceProduct.Add(productDto);

            return Ok("Product Added successfully.");
        }
        
        [HttpGet]
        [Route("/show-all-products")]
        public ActionResult<IEnumerable<ProductDto>> GetAllProducts()
        {
            return Ok(_applicationServiceProduct.GetAll());
        }

        [HttpGet]
        [Route("/show-one-product/{id}")]
        public ActionResult<ProductDto> GetOneProduct(Guid id)
        {
            var product = _applicationServiceProduct.GetById(id);

            if (product == null)
                return NotFound("Product by Id not found.");

            return Ok(product);
        }

        [HttpDelete]
        [Route("/delete-product/{id}")]
        public ActionResult DeleteProduct(Guid id)
        {
            var product = _applicationServiceProduct.GetById(id);

            if (product == null)
                return NotFound("Product by Id not found");

            _applicationServiceProduct.Delete(product);

            return Ok("Product deleted successfully.");
        }

        [HttpPut]
        [Route("/update-product/{id}")]
        public ActionResult UpdateProdut(Guid id,
                                         [FromBody] ProductDto productDto)
        {
            var product = _applicationServiceProduct.GetById(id);

            if (product == null)
                return NotFound("Product by Id not found.");
            
            _applicationServiceProduct.Update(productDto);

            return Ok("Product updating successfully.");
        }
    }
}