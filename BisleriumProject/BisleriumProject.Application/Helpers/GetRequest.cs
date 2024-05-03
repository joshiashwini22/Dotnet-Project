using BisleriumProject.Domain.Shared;
using System.Linq.Expressions;

namespace BisleriumProject.Application.Helpers
{
    public class GetRequest<T> where T : BaseEntity
    {
        public Func<IQueryable<T>, IQueryable<T>>? Filter { get; set; }
        public Func<IQueryable<T>, IOrderedQueryable<T>>? OrderBy { get; set; } = null;
        public int? Skip { get; set; } = null;
        public int? Take { get; set; } = null;
    }
}
