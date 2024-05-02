using BisleriumProject.Application.Helpers;
using BisleriumProject.Domain.Shared;

namespace BisleriumProject.Application.Common.Interface.IRepositories
{
    public interface IRepositoryBase<T> where T : class
    {
        Task<T> Add(T entity);
        Task<T> Update(T entity);
        void Delete(string entityId);
        Task<T>? GetById(string entityId);
    }
}
