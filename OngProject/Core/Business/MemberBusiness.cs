using OngProject.Core.Interfaces;
using OngProject.Core.Mapper;
using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OngProject.Core.Business
{
    public class MemberBusiness : IMemberBusiness
    {
        private readonly IUnitOfWork _unitOfWork;

        public MemberBusiness(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<MembersDTO> GetAll()
        {
            var members = _unitOfWork.MembersRepository.GetAll();
            var dtos = new List<MembersDTO>();

            foreach (var member in members)
            {
                dtos.Add(MemberMapper.ToPublicDTO(member));
            }

            return dtos;
        }

        public Task<Member> GetById(int id)
        {
            return _unitOfWork.MembersRepository.GetById(id);
        }

        public async Task Add(MembersDTO members)
        {   

            //var member = await _unitOfWork.MembersRepository.GetById(members.Id);


            if (members.Name is null && members.Image is null)
            {
                throw new Exception("The information is incorrect");
            }

            Member newMember = new Member()
            {
                Name = members.Name,
                FacebookUrl = members.FacebookUrl,
                InstagramUrl = members.InstagramUrl,
                LinkedInUrl = members.LinkedInUrl,
                Description = members.Description                
            };

            await _unitOfWork.MembersRepository.Add(newMember);
            _unitOfWork.SaveChanges();
        }

        public async Task<bool> Update(Member members)
        {
            await _unitOfWork.MembersRepository.Update(members);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            await _unitOfWork.MembersRepository.Delete(id);
            return true;
        }

    }
}
