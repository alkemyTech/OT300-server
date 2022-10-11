using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging.Abstractions;
using OngProject.Core.Interfaces;
using OngProject.Core.Mapper;
using OngProject.Core.Models.DTOs;
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
        //public User Insert(User request)
        //{
        //    throw new NotImplementedException();
        //}

        public IEnumerable<UserGetDTO> GetAllUsers()
        {
            var Users = _unitOfWork.UserRepository.GetAll();
            var UserDTOs = new List<UserGetDTO>();

            foreach (var user in Users)
            {
                UserDTOs.Add(UserMapper.ToUserDTO(user));
            }

            return UserDTOs;
        }

        public async Task<UserGetDTO> GetById(int id)
        {
            var user = await _unitOfWork.UserRepository.GetById(id);
            var userDto = UserMapper.ToUserDTO(user);

            return userDto;
        }
        public async Task<UserPatchDTO> Update(int id, UserPatchDTO user)
        {
            User toUpdate = await _unitOfWork.UserRepository.GetById(id);

            if (toUpdate is null)
                return null;

            toUpdate.PatchToEntity(user);

            await _unitOfWork.UserRepository.Update(toUpdate);
            await _unitOfWork.SaveChangesAsync();

            return user;
        }

        public async Task Delete(int id)
        {
            await _unitOfWork.UserRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
        }

   

        
    }
}