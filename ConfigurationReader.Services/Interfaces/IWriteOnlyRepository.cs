using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConfigurationReader.Interfaces
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
