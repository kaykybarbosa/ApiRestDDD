namespace RestApiDDD.Domain.Core.Interfaces.Repositiories
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        Task<TEntity> Add(TEntity entity);
        Task<TEntity> Delete(TEntity entity);
        Task<TEntity> Update(TEntity entity);
        IEnumerable<TEntity> GetAll();
        Task<TEntity> GetById(Guid id);
    }
}