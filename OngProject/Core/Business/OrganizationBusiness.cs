using OngProject.Core.Interfaces;
using OngProject.Core.Mapper;
using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Core.Business
{
    public class OrganizationBusiness : IOrganizationBusiness
    {

        private readonly IUnitOfWork _unitOfWork;
        private IImageStorageHerlper _imageStorageHerlper;

        public OrganizationBusiness(IUnitOfWork unitOfWork, IImageStorageHerlper imageStorageHerlper)
        {
            _unitOfWork = unitOfWork;
            _imageStorageHerlper = imageStorageHerlper;
        }

        public IEnumerable<OrganizationDTO> GetAll()
        {
            return (IEnumerable<OrganizationDTO>)_unitOfWork.OrganizationRepository.GetAll();
        }

        public async Task<Organization> GetById(int id)
        {
            return await _unitOfWork.OrganizationRepository.GetById(id);
        }

        public async Task<Organization> Insert(Organization organization)
        {
            await _unitOfWork.OrganizationRepository.Add(organization);
            _unitOfWork.SaveChanges();
            return organization;
        }


        public async Task<OrganizationPublicDTO> Update(OrganizationPublicDTO organization)
        {
            var orgToUpdate = _unitOfWork.OrganizationRepository.GetAll().First();
            if (orgToUpdate != null)
            {
                //set new values;
                orgToUpdate.SetNewValues(organization);
                //set image
                if (organization.ImageStream is not null || organization.ImageStream.Length !=0)
                {
                    orgToUpdate.Img  =
                        await _imageStorageHerlper.UploadImageAsync(organization.ImageStream, $"Organization-{organization.Name}.jpg".Replace(" ","-"));
                }

                var updated = await _unitOfWork.OrganizationRepository.Update(orgToUpdate);
                await _unitOfWork.SaveChangesAsync();
                return updated.ToPublicDTO();
            }
            else
            {
                return null;
            }

        }

        public async Task<bool> Delete(int id)
        {
            var orgToDelete = await _unitOfWork.OrganizationRepository.GetById(id);
            if (orgToDelete != null)
            {
                orgToDelete.IsDeleted = true;
                _unitOfWork.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// US-30 | Public data 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.Exception"></exception>
        public OrganizationPublicDTO GetPublicInfo()
        {
            try
            {
                Organization org = _unitOfWork.OrganizationRepository.GetAll().First();

                var dto = org.ToPublicDTO();
                return dto;
            }
            catch (Exception e)
            {
                //Log(e);
                throw new Exception("Cannot retrieve organization public details, see inner excepction", e);
            }
        }

    
    }
}
