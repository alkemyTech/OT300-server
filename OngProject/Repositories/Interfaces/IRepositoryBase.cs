using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OngProject.Entities;

namespace OngProject.Repositories.Interfaces
{
   public interface IRepositoryBase<T> where T : BaseEntity
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        T Insert(T entity);
        T Update(T entity);
        void Delete(T entity);

    }
}