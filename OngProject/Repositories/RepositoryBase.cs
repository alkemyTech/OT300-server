using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OngProject.DataAccess;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;

namespace OngProject.Repositories
{
    public  class RepositoryBase<T> : IRepositoryBase<T> where T : BaseEntity
    {
        private readonly OngDbContext _dbContext;

        public RepositoryBase(OngDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IEnumerable<T> GetAll()
        {
            return _dbContext.Set<T>().ToList();
        }

        public T GetById(int id)
        {
            var result = _dbContext.Set<T>().Find(id);
            return result;
        }

        public T Insert(T entity)
        {
            var result = _dbContext.Set<T>().Add(entity);
            return result.Entity;
        }

        public T Update(T entity)
        {
            var result = _dbContext.Set<T>().Update(entity);
            return result.Entity;
        }

        public void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
        }
    }
}
