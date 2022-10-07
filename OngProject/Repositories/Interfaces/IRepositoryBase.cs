using System.Collections.Generic;
using System.Threading.Tasks;
using OngProject.Entities;

namespace OngProject.Repositories.Interfaces
{
    public interface IRepositoryBase<T> where T : BaseEntity
    {
        PagedList<T> GetAll(int page);
        IEnumerable<T> GetAll();
        Task<T> GetById(int id);
        Task<T> Add(T entity);
        Task<T> Update(T entity);
        Task<bool> Delete(int id);
        Task<bool> EntityExist(int id);

    }
}