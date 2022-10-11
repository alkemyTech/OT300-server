using System.Collections.Generic;
using System.Threading.Tasks;
using OngProject.Entities;

namespace OngProject.Repositories.Interfaces
{
    public interface IRepositoryBase<T> where T : BaseEntity
    {
        Task<T> Add(T entity);
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAll(bool includeDeleted);
        PagedList<T> GetAll(int page);
        PagedList<T> GetAll(int page, bool includeDeleted);
        Task<T> GetById(int id);
        Task<T> Update(T entity);
        Task<bool> Delete(int id);
        Task<bool> EntityExist(int id);
        Task<bool> EntityExist(int id, bool includeDeleted);

    }
}