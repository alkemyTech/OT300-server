﻿using System;
using System.Collections.Generic;
using OngProject.Entities;
using OngProject.Services.Interfaces;

namespace OngProject.Services
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RoleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public Role Create(Role request)
        {
            request.CreatedAt = DateTime.UtcNow;
            var result = _unitOfWork.RoleRepository.Insert(request);
            _unitOfWork.SaveChanges();
            return result;
        }

        public IEnumerable<Role> GetAll()
        {
            return _unitOfWork.RoleRepository.GetAll();
        }

        public Role GetById(int id)
        {
            var result = _unitOfWork.RoleRepository.GetById(id);
            return result;
        }

        public Role Update(Role role)
        {
            role.LastEditedAt = DateTime.UtcNow;
            var result = _unitOfWork.RoleRepository.Update(role);
            _unitOfWork.SaveChanges();
            return result;
        }

        public void Delete(Role role)
        {
            role.IsDeleted = true;
            role.LastEditedAt = DateTime.UtcNow;
            _unitOfWork.RoleRepository.Update(role);
            _unitOfWork.SaveChanges();
        }
    }
}