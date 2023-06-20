namespace RestApiDDD.Domain.Core.Interfaces.Repositiories
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        void Add(TEntity entity);
        void Delete(TEntity entity);
        void Update(TEntity entity);
        IEnumerable<TEntity> GetAll();
        TEntity GetById(Guid id);
    }
}
