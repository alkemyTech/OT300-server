using OngProject.Core.Interfaces;
using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Mvc;

namespace OngProject.Core.Business
{
    public class OrganizationBusiness : IOrganizationBusiness
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly OrganizationBusiness _organizationBusiness;

        public OrganizationBusiness(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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

        
        public async Task<Organization> Update(int id, Organization organization)
        {
            var orgToUpdate = await _unitOfWork.OrganizationRepository.GetById(id);
            if (orgToUpdate != null)
            {
                _unitOfWork.OrganizationRepository.Update(organization);
                _unitOfWork.SaveChanges();
                return organization;
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
    }
}
