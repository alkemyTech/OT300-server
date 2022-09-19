using System;
using System.Collections.Generic;
using OngProject.Core.Interfaces;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;

namespace OngProject.Core.Business
{
    public class UserBusiness : IUserBusiness
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserBusiness(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public User Insert(User request)
        {
            request.CreatedAt = DateTime.UtcNow;
            var result = _unitOfWork.UserRepository.Insert(request);
            _unitOfWork.SaveChanges();
            return result;
        }

        public IEnumerable<User> GetAll()
        {
            return _unitOfWork.UserRepository.GetAll();
        }

        public User GetById(int id)
        {
            var result = _unitOfWork.UserRepository.GetById(id);
            return result;
        }

        public User Update(User user)
        {
            var result = _unitOfWork.UserRepository.Update(user);
            _unitOfWork.SaveChanges();
            return result;
        }

        public void Delete(User user)
        {

            user.IsDeleted = true;
            user.LastEditedAt = DateTime.UtcNow;
            _unitOfWork.UserRepository.Update(user);
            _unitOfWork.SaveChanges();
        }
    }
}