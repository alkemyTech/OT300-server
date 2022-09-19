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
            var result = await _unitOfWork.RolesRepository.Add(request);
            await _unitOfWork.SaveChangesAsync();
            return result;
        }

        public IEnumerable<Role> GetAll()
        {
            return _unitOfWork.RolesRepository.GetAll();
        }

        public async Task<Role> GetById(int id)
        {
            var result = await _unitOfWork.RolesRepository.GetById(id);
            return result;
        }

        public async Task<Role> Update(Role role)
        {
            role.LastEditedAt = DateTime.UtcNow;
            var result = await _unitOfWork.RolesRepository.Update(role);
            await _unitOfWork.SaveChangesAsync();
            return result;
        }

        public async Task Delete(Role role)
        {
            await _unitOfWork.RolesRepository.Delete(role.Id);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}