using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OngProject.Core.Interfaces;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;

namespace OngProject.Core.Business
{
    public class RoleBusiness : IRoleBusiness
    {
        private readonly IUnitOfWork _unitOfWork;

        public RoleBusiness(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Role> Create(Role request)
        {
            request.CreatedAt = DateTime.UtcNow;
            var result = await _unitOfWork.RoleRepository.Add(request);
            await _unitOfWork.SaveChangesAsync();
            return result;
        }

        public IEnumerable<Role> GetAll()
        {
            return _unitOfWork.RoleRepository.GetAll();
        }

        public async Task<Role> GetById(int id)
        {
            var result = await _unitOfWork.RoleRepository.GetById(id);
            return result;
        }

        public async Task<Role> Update(Role role)
        {
            role.LastEditedAt = DateTime.UtcNow;
            var result = await _unitOfWork.RoleRepository.Update(role);
            await _unitOfWork.SaveChangesAsync();
            return result;
        }

        public async Task Delete(int id)
        {
            await _unitOfWork.RoleRepository.Delete(id);

            await _unitOfWork.SaveChangesAsync();
        }
        public async Task<bool> DoesExist(int id)
        {
            return await _unitOfWork.RoleRepository.EntityExist(id);
        }
    }
}