using Microsoft.EntityFrameworkCore;
using OngProject.DataAccess;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace OngProject.Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : BaseEntity

    {
        private readonly OngDbContext _dbContext;
        protected readonly DbSet<T> _entities;

        public RepositoryBase(OngDbContext dbContext)
        {
            _dbContext = dbContext;
            _entities = dbContext.Set<T>();
        }

        public async Task<T> GetById(int id)
        {
            var entity = await _entities.FindAsync(id);
            return (entity == null || entity.IsDeleted) ? null : entity;
        }

        public async Task<T> Add(T entity)
        {
            var date = DateTime.UtcNow;
            entity.CreatedAt = date;
            entity.LastEditedAt = date;
            var result = await _entities.AddAsync(entity);

            return result.Entity;
        }

        public async Task<T> Update(T entity)
        {
            var result = _dbContext.Entry(entity).State = EntityState.Modified;
            entity.LastEditedAt = DateTime.UtcNow;
            //TODO  si no esta en el contexto por UOF excepcion al savechanges
            //TODO  Para que se neceista devolver T?
            return await Task.FromResult(entity);
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var entity = await _entities.FindAsync(id);
                if (entity == null || entity.IsDeleted) return false;

                entity.IsDeleted = true;
                entity.LastEditedAt = DateTime.UtcNow;

                _entities.Update(entity);
                return true;
            }
            catch (Exception e)
            {
                throw new Exception("Error on Repository.Delete", e);
            }
        }

        public Task<bool> EntityExist(int id)
        {
            return _entities.AnyAsync(x => x.Id == id && !x.IsDeleted);
        }

        public PagedList<T> GetAll(int pageNumber = 1 /*, int pageSize=10*/)
        {
            return PagedList<T>.Create(_entities.Where(x => x.IsDeleted == false), pageNumber, 10);
        }
        public IEnumerable<T> GetAll()
        {
            return _entities.Where(x => x.IsDeleted == false);
        }
    }
}
