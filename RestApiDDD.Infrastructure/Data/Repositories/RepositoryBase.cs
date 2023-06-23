using Microsoft.EntityFrameworkCore;
using RestApiDDD.Domain.Core.Interfaces.Repositiories;

namespace RestApiDDD.Infrastructure.Data.Repositories
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
    {
        private readonly ConnectionContext _context;

        public RepositoryBase(ConnectionContext context) => _context = context;

        public async Task<TEntity> Add(TEntity entity)
        {
           _context.Set<TEntity>().Add(entity);
           _context.SaveChanges();
            
           return entity;
        }
        public async Task<TEntity> Delete(TEntity entity)
        {
           _context.Set<TEntity>().Remove(entity);
           _context.SaveChanges();

            return entity;
        }
        public IEnumerable<TEntity> GetAll() => _context.Set<TEntity>().ToList();

        public async Task<TEntity?> GetById(Guid id) => _context.Set<TEntity>().Find(id);

        public async Task<TEntity> Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();

            return entity;
        }
    }
}