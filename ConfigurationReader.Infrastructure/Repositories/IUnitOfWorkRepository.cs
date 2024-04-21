using ConfigurationReader.Infrastructure.Entity;
using ConfigurationReader.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationReader.Infrastructure.Repositories
{
    public interface IUnitOfWorkRepository
    {
        Task CommitAsync();

        void Commit();
    }
}
