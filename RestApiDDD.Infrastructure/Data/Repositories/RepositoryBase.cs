using Microsoft.EntityFrameworkCore;
using RestApiDDD.Domain.Core.Interfaces.Repositiories;

namespace RestApiDDD.Infrastructure.Data.Repositories
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
    {
        private readonly ConnectionContext _context;

        public RepositoryBase(ConnectionContext context) => _context = context;

        public void Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            _context.SaveChanges();
        }

        public void Delete(TEntity entity)
        {
           _context.Set<TEntity>().Remove(entity);
           _context.SaveChanges();
        }

        public IEnumerable<TEntity> GetAll() => _context.Set<TEntity>().ToList();

        public TEntity GetById(Guid id) => _context.Set<TEntity>().Find(id);

        public void Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}