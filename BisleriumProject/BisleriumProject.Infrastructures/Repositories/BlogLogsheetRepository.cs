using BisleriumProject.Application.Common.Interface.IRepositories;
using BisleriumProject.Domain.Entities;
using BisleriumProject.Infrastructures.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BisleriumProject.Infrastructures.Repositories
{
    public class BlogLogsheetRepository : RepositoryBase<BlogLogsheet>, IBlogLogsheetRepository
    {
        private readonly AppDbContext _appDbContext;

        public BlogLogsheetRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }
    }
}
