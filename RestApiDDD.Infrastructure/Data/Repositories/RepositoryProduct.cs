using RestApiDDD.Domain.Core.Interfaces.Repositiories;
using RestApiDDD.Domain.Entities;

namespace RestApiDDD.Infrastructure.Data.Repositories
{
    public class RepositoryProduct : RepositoryBase<Product>, IRepositoryProduct
    {
        private readonly ConnectionContext _context;

        public RepositoryProduct(ConnectionContext context)
            : base(context) => _context = context;
    }
}