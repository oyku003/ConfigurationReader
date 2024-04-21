using ConfigurationReader.Infrastructure.Entity;
using ConfigurationReader.Infrastructure.Repositories;
using ConfigurationReader.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationReader.Data.UnitOfWorks
{
    public class UnitOfWorkRepository : IUnitOfWorkRepository
    {
        private readonly AppDbContext _appDbContext;
        public UnitOfWorkRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public void Commit()
            => _appDbContext.SaveChanges();

        public async Task CommitAsync()
            => await _appDbContext.SaveChangesAsync();
       
    }
}
