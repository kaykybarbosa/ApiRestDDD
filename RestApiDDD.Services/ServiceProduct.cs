using RestApiDDD.Domain.Core.Interfaces.Repositiories;
using RestApiDDD.Domain.Core.Interfaces.Services;
using RestApiDDD.Domain.Entities;

namespace RestApiDDD.Services
{
    public class ServiceProduct : ServiceBase<Product>, IServiceProduct
    {
        private readonly IRepositoryProduct _repositoyProduct;
        public ServiceProduct(IRepositoryProduct repositoyProduct)
            :base(repositoyProduct)
        {
            _repositoyProduct = repositoyProduct;
        }
    }
}