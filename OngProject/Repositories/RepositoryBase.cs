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
            var result = await _entities.AddAsync(entity);

            return result.Entity;
        }

        public async Task<T> Update(T entity)
        {
            var result = await _entities.AddAsync(entity);

            return result.Entity;
        }

        public async Task Delete(T entity)
        {
            /* If we check in the controller if the entity is null then we sould retutn 404
             if is not null then we call the delete method and pass the entity to it.*/

            entity.IsDeleted = true;
            entity.LastEditedAt = DateTime.UtcNow;
            _entities.Update(entity);

            await _dbContext.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll()
        {
            return _entities;
        }
    }
}