using BisleriumProject.Application.Helpers;
using BisleriumProject.Domain.Entities;
using BisleriumProject.Domain.Shared;

namespace BisleriumProject.Application.Common.Interface.IRepositories
{
    public interface IRepositoryBase<T> where T : class
    {
        Task<T> Add(T entity);
        Task<T> Update(T entity);
        Task Delete(T entity);

        Task<T>? GetById(object entityId);
        Task<IEnumerable<T>> GetAll(GetRequest<T>? request);
        Task SaveChangesAsync();


    }
}
