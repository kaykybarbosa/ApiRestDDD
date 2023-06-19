using RestApiDDD.Domain.Core.Interfaces.Repositiories;
using RestApiDDD.Domain.Core.Interfaces.Services;
using RestApiDDD.Domain.Entities;

namespace RestApiDDD.Services
{
    public class SerivceClient : ServiceBase<Client>, IServiceClient
    {
        private readonly IRepositoryClient _repositoryClient;
        public SerivceClient(IRepositoryClient _repositoryClient)
            :base(_repositoryClient)
        {
            _repositoryClient = _repositoryClient;
        }
    }
}