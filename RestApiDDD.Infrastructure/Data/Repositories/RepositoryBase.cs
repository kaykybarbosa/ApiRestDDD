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
           await _context.Set<TEntity>().AddAsync(entity);
           _context.SaveChanges();
            
           return entity;
        }
        public async Task<TEntity> Delete(TEntity entity)
        {
           _context.Set<TEntity>().Remove(entity);
           _context.SaveChanges();

            return entity;
        }
        public async Task<IEnumerable<TEntity>> GetAll() => await _context.Set<TEntity>().ToListAsync();

        public async Task<TEntity?> GetByEmail(string email) => await _context.Set<TEntity>().FindAsync(email);

        public async Task<TEntity?> GetById(Guid id) => await _context.Set<TEntity>().FindAsync(id);
        public async Task<TEntity> Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();

            return entity;
        }
    }
}