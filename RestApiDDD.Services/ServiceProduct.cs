using RestApiDDD.Domain.Core.Interfaces.Repositiories;
using RestApiDDD.Domain.Core.Interfaces.Services;
using RestApiDDD.Domain.Entities;

namespace RestApiDDD.Services
{
    public class ServiceProduct : ServiceBase<Product>, IServiceProduct
    {
        private readonly IRepositoyProduct _repositoyProduct;
        public ServiceProduct(IRepositoyProduct repositoyProduct)
            :base(repositoyProduct)
        {
            _repositoyProduct = repositoyProduct;
        }
    }
}