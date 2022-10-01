using OngProject.Core.Helper;
using OngProject.Core.Interfaces;
using OngProject.Core.Mapper;
using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Amazon.S3.Util.S3EventNotification;

namespace OngProject.Core.Business
{
    public class MemberBusiness : IMemberBusiness
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IImageStorageHerlper _imageStorageHerlper;

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

        public async Task<MembersDTO> Add(MembersDTO members)
        {   

            Member memberEntity = new Member();

            var fileName = "Member-" + members.Name + ".jpg";

            memberEntity = members.DtoToMember();

            if (members.Image.Length == 0)
            {
                memberEntity.Image = "";
            }
            else
            {
                memberEntity.Image = await _imageStorageHerlper.UploadImageAsync(members.Image, fileName);
            }

            
            await _unitOfWork.MembersRepository.Add(memberEntity);
            await _unitOfWork.SaveChangesAsync();

            return members;
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
