using BisleriumProject.Application.Common.Interface.IRepositories;
using BisleriumProject.Application.Helpers;
using BisleriumProject.Domain.Shared;
using BisleriumProject.Infrastructures.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BisleriumProject.Infrastructures.Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected AppDbContext _appDbContext;

        

        public RepositoryBase(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public Task<T> Add(T entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(string entityId)
        {
            throw new NotImplementedException();
        }

        public Task<T>? GetById(string entityId)
        {
            throw new NotImplementedException();
        }

        public Task<T> Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
