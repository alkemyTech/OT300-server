using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OngProject.Core.Interfaces
{
    public interface IOrganizationBusiness
    {
        IEnumerable<OrganizationDTO> GetAll();

        Task<Organization> GetById(int id);

        Task<Organization> Insert(Organization organization);

        Task<Organization> Update(int id, Organization organization);

        Task<bool> Delete(int id);
    }
}
