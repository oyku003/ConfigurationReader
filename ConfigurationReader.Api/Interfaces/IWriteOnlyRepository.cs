
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationReader.Api.Interfaces
{
    public interface IWriteOnlyRepository<TEntity> where TEntity : class
    {
        Task AddAsync(TEntity entity);

        Task AddRangeAsync(IEnumerable<TEntity> entities);
        void Remove(TEntity entity);

        void RemoveRange(IEnumerable<TEntity> entitiy);

        TEntity Update(TEntity entity);
    }
}
