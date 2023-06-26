using RestApiDDD.Application.DTOs.Request;
using RestApiDDD.Application.DTOs.Response;
using RestApiDDD.Application.Interfaces;
using RestApiDDD.Application.Interfaces.Mappers;
using RestApiDDD.Domain.Core.Interfaces.Services;

namespace RestApiDDD.Application
{
    public class ApplicationServiceProduct : IApplicationServiceProduct
    {
        private readonly IServiceProduct _serviceProduct;
        private readonly IMapperProduct _mapperProduct;

        public ApplicationServiceProduct(IServiceProduct serviceProduct,
                                         IMapperProduct mapperProduct)
        {
            _serviceProduct = serviceProduct;
            _mapperProduct = mapperProduct;
        }
        public async Task<BaseResponseDTO> Add(ProductRequestDTO productDto)
        {
            try
            {
                var product = _mapperProduct.MapperDtoToEntity(productDto);
                await _serviceProduct.Add(product);

                return new BaseResponseDTO(message: "Product added successfully.", success: true);
            }
            catch (Exception e)
            {
                return new BaseResponseDTO(message: "Error while saving product.", success: false, error: e.Message) ;
            }
        }

        public async Task<BaseResponseDTO> Delete(Guid id)
        {
            try
            {
                BaseResponseDTO response = new();
                var product = _serviceProduct.GetById(id);

                if (product == null) {
                    response.Message = "Product by Id not found.";
                    response.Success = false; 
                }
                else { 
                    await _serviceProduct.Delete(product.Result);

                    response.Message = "Product deleted successfully.";
                    response.Success = true; 
                }

                return response;
            }
            catch (Exception e)
            {
                return new BaseResponseDTO(message: "Erro while deleting product.", success: false, error: e.Message);
            }
        }

        public IEnumerable<ProductResponseDTO> GetAll()
        {
            var products = _serviceProduct.GetAll();

            return _mapperProduct.MapperListProductDto(products);
        }

        public async Task<ProductResponseDTO> GetById(Guid id)
        {
            try
            {
                ProductResponseDTO response = new();
                var product = await _serviceProduct.GetById(id);
                
                if (product == null)
                {
                    response.Message = "Product by Id not found.";
                    response.Success = false;
                }
                else
                {
                    response.Id = product.Id;
                    response.Name = product.Name;
                    response.Price = product.Price;
                    response.IsAvaiable = product.IsAvaiable;
                    response.Message = "Found successfully.";
                    response.Success = true;
                }

                return response;
            }
            catch (Exception e)
            {
                return new ProductResponseDTO()
                {
                    Message = "Error while fetching the client.",
                    Success = false,
                    Error = e.Message
                };
            }
        }

        public async Task<BaseResponseDTO> Update(Guid id, ProductRequestDTO productDto)
        {
            try
            {
                BaseResponseDTO response = new();
                var product = await _serviceProduct.GetById(id);

                if(product == null)
                {
                    response.Message = "Product by Id not found.";
                    response.Success = false;
                }
                else
                {
                    product.Name = productDto.Name;
                    product.Price = productDto.Price;

                    await _serviceProduct.Update(product);

                    response.Message = "Product updating successfully.";
                    response.Success = true;
                }

                return response;
            }
            catch (Exception e)
            {
                return new BaseResponseDTO()
                {
                    Message = "Error while updating product.",
                    Success = false,
                    Error = e.Message
                };
            }
        }
    }
}