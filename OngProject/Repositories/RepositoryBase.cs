using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OngProject.DataAccess;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;


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
            return await _entities.FindAsync(id);
        }

        public async Task<T> Add(T entity)
        {
            entity.CreatedAt = DateTime.UtcNow;
            entity.LastEditedAt = DateTime.UtcNow;
            var result = await _entities.AddAsync(entity);

            return result.Entity;
        }

        public async Task<T> Update(T entity)
        {
            var result = _dbContext.Entry(entity).State = EntityState.Modified;
            //TODO: si no esta en el contexto por UOF excepcion al savechanges
            //TODO: Para que se neceista devolver T?
            return entity;
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var entity = await _entities.FindAsync(id);

                entity.IsDeleted = true;
                entity.LastEditedAt = DateTime.UtcNow;

                _entities.Update(entity);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        public Task<bool> EntityExist(int id)
        {
            return _entities.AnyAsync(x => x.Id == id);
        }

        public PagedList<T> GetAll(int pageNumber = 1 /*, int pageSize=10*/)
        {
            return PagedList<T>.Create(_entities, pageNumber, 10);
        }
        public IEnumerable<T> GetAll()
        {
            return _entities;
        }
    }
}
