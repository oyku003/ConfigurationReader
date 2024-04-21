using ConfigurationReader.Infrastructure.Entity;
using ConfigurationReader.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationReader.Services.Data.Repositories
{
    public class ReadOnlyRepository<TEntity> : IReadOnlyRepository<TEntity> where TEntity : class//, IBaseEntity<TId>
    {
        protected readonly DbContext dbContext;
        private readonly DbSet<TEntity> dbSet;

        public ReadOnlyRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
            this.dbSet = dbContext.Set<TEntity>();
        }
        public async Task<IEnumerable<TEntity>> Where(Expression<Func<TEntity, bool>> predicate)
        {
            return await dbSet.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await dbSet.ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await dbSet.FindAsync(id);//birden fazla primary key olan tablolar için dizi de verebilirdik
        }
        public async Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await dbSet.SingleOrDefaultAsync(predicate);
        }
    }
}
