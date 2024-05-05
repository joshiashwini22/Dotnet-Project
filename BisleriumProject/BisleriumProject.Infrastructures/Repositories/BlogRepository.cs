using BisleriumProject.Application.Common.Interface.IRepositories;
using BisleriumProject.Domain.Entities;
using BisleriumProject.Infrastructures.Persistence;

namespace BisleriumProject.Infrastructures.Repositories
{
    public class BlogRepository : RepositoryBase<Blog>, IBlogRepository
    {
        private readonly AppDbContext _appDbContext;

        public BlogRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }
    }
}
