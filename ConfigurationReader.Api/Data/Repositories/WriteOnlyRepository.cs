
using ConfigurationReader.Api.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationReader.Api.Data.Repositories
{
    public class WriteOnlyRepository<TEntity> : IWriteOnlyRepository<TEntity> where TEntity : class//, IBaseEntity<TId>
    {
        protected readonly DbContext dbContext;
        private readonly DbSet<TEntity> dbSet;

        public WriteOnlyRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
            this.dbSet = dbContext.Set<TEntity>();
        }
        public async Task AddAsync(TEntity entity)
        {
            await dbSet.AddAsync(entity);
            dbContext.SaveChanges();
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await dbSet.AddRangeAsync(entities);
            dbContext.SaveChanges();
        }       

        public void Remove(TEntity entity)
        {
            dbSet.Remove(entity);
            dbContext.SaveChanges();
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            dbSet.RemoveRange(entities);
            dbContext.SaveChanges();
        }
       

        public TEntity Update(TEntity entity)
        {
            dbContext.Entry(entity).State = EntityState.Modified;
            dbContext.SaveChanges();
            return entity;
        }
    }
}
