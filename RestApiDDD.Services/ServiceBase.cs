using RestApiDDD.Domain.Core.Interfaces.Repositiories;
using RestApiDDD.Domain.Core.Interfaces.Services;

namespace RestApiDDD.Services
{
    public class ServiceBase<TEntity> : IServiceBase<TEntity> where TEntity : class
    {
        private readonly IRepositoryBase<TEntity> _repositoryBase;
        public ServiceBase(IRepositoryBase<TEntity> repositoryBase)
        {
            _repositoryBase = repositoryBase;
        }

        public async Task<TEntity> Add(TEntity entity)
        {
            await _repositoryBase.Add(entity);
            
            return entity;
        }

        public async Task<TEntity> Delete(TEntity entity)
        {
            await _repositoryBase.Delete(entity);

            return entity;
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _repositoryBase.GetAll();
        }

        public async Task<TEntity?> GetByEmail(string email)
        {
            return await _repositoryBase.GetByEmail(email);
        }

        public async Task<TEntity?> GetById(Guid id)
        {
            return await _repositoryBase.GetById(id);
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            await _repositoryBase.Update(entity);

            return entity;
        }
    }
}