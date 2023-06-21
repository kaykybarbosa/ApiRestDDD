using RestApiDDD.Domain.Core.Interfaces.Repositiories;
using RestApiDDD.Domain.Entities;

namespace RestApiDDD.Infrastructure.Data.Repositories
{
    public class RepositoryClient : RepositoryBase<Client>, IRepositoryClient
    {
        private readonly ConnectionContext _context;

        public RepositoryClient(ConnectionContext context)
            : base(context) => _context = context;
    }
}
