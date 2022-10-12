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

        public MemberBusiness(IUnitOfWork unitOfWork, IImageStorageHerlper imageStorageHerlper)
        {
            _unitOfWork = unitOfWork;
            _imageStorageHerlper = imageStorageHerlper;
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

            if (members.Image.Length == 0 || members.Image is null)
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

        public async Task<MemberUpdateDTO> Update(int id, MemberUpdateDTO memberUpdateDto)
        {
            var memberUp = await _unitOfWork.MembersRepository.GetById(id);

            if (memberUpdateDto.Name == null) memberUpdateDto.Name = memberUp.Name;

            if (memberUpdateDto.ImageFile is null || memberUpdateDto.ImageFile.Length == 0)
            {
                memberUp.Image = "";
            }

            else
            {
                var fileName = "Member -" + memberUpdateDto.Name + ".jpg";

                memberUp.Image = await _imageStorageHerlper.UploadImageAsync(memberUpdateDto.ImageFile, fileName);
            }

            memberUp.UpdateDtoToMember(memberUpdateDto);

            await _unitOfWork.MembersRepository.Update(memberUp);

            await _unitOfWork.SaveChangesAsync();

            return memberUpdateDto;
        }
    

        public async Task Delete(int id)
        {
            var members = await _unitOfWork.MembersRepository.GetById(id);

            if (members == null) throw new Exception("Member does not exist");
            

            await _unitOfWork.MembersRepository.Delete(id);            

            await _unitOfWork.SaveChangesAsync();   
        }

        public async Task<bool> DoesExist(int id)
        {
            return await _unitOfWork.MembersRepository.EntityExist(id);
        }
    }
}