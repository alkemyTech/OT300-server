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
    public class ActivityBusiness : IActivityBusiness
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IImageStorageHerlper _imageStorageHerlper;

        public ActivityBusiness(IUnitOfWork unitOfWork, IImageStorageHerlper imageStorageHerlper)
        {
            _unitOfWork = unitOfWork;
            _imageStorageHerlper = imageStorageHerlper;
        }

        public IEnumerable<Activity> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Activity> GetById(int id)
        {
            return _unitOfWork.ActivityRepository.GetById(id);
        }

        public async Task<ActivityDTO> Add(ActivityDTO activityDTO)
        {
            var fileName = "Activity-" + activityDTO.Name + ".jpg";

            Activity activities = new Activity()
            {
                Name = activityDTO.Name,
                Content = activityDTO.Content,

            };

            if (activityDTO.ImageFile.Length == 0 || activityDTO.ImageFile is null)
            {
                activities.Image = "";
            }
            else
            {
                activities.Image = await _imageStorageHerlper.UploadImageAsync(activityDTO.ImageFile, fileName);

            }

            await _unitOfWork.ActivityRepository.Add(activities);
            await _unitOfWork.SaveChangesAsync();

            return activityDTO;
        }

        public async Task<ActivityUpdateDTO> Update(int id, ActivityUpdateDTO activityUpdateDto)
        {

            var activityUp = await _unitOfWork.ActivityRepository.GetById(id);

            if (activityUpdateDto.Name == null) activityUpdateDto.Name = activityUp.Name;

            if (activityUpdateDto.Content == null) activityUpdateDto.Content = activityUp.Content;

            if (activityUpdateDto.ImageFile is null || activityUpdateDto.ImageFile.Length == 0)
            {
                activityUp.Image = "";
            }
            
            else
            {
                var fileName = "Activity-" + activityUpdateDto.Name + ".jpg";

                activityUp.Image = await _imageStorageHerlper.UploadImageAsync(activityUpdateDto.ImageFile, fileName);
            }
            
            activityUp.UpdateDtoToActivity(activityUpdateDto);

            await _unitOfWork.ActivityRepository.Update(activityUp);

            await _unitOfWork.SaveChangesAsync();

            return activityUpdateDto;
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }


        public async Task<bool> DoesExist(int id)
        {
            return await _unitOfWork.ActivityRepository.EntityExist(id);
        }

    }
}